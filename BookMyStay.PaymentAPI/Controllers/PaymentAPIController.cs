using AutoMapper;
using BookMyStay.PaymentAPI.Data;
using BookMyStay.PaymentAPI.Models;
using BookMyStay.PaymentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace BookMyStay.PaymentAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize]
    public class PaymentAPIController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseDTO _responseDTO;
        private IMapper _mapper;
        private IListingService _listingService;


        public PaymentAPIController(
                ApplicationDBContext ctx,
                IMapper mapper,
                IListingService listingService)
        {
            _dbContext = ctx;
            _mapper = mapper;
            _responseDTO = new APIResponseDTO();
            _listingService = listingService;
            _responseDTO = new APIResponseDTO();
        }

        [HttpPost("create")]
        public async Task<APIResponseDTO> CreatePaymentRequest([FromBody] BookingDTO bookingDTO)
        {
            try
            {
                PaymentItemDTO paymentItemDTO = _mapper.Map<PaymentItemDTO>(bookingDTO.BookingItemDTO);
                paymentItemDTO.PaymentDate = DateTime.Now;
                paymentItemDTO.PaymentStatus = "INPROCESS";

                paymentItemDTO.PaymentItemDetails = _mapper.Map<IEnumerable<PaymentItemDetailsDTO>>(bookingDTO.BookingDetailsDTO); //BookingDetailsDTO is null so need to refill

                PaymentItem paymentItem = _dbContext.PaymentItem.Add(_mapper.Map<PaymentItem>(paymentItemDTO)).Entity;
                await _dbContext.SaveChangesAsync();

                paymentItemDTO.PaymentRequestId = paymentItem.PaymentRequestId;

                _responseDTO.HasError = false;
                _responseDTO.Info = "";
                _responseDTO.Result = paymentItemDTO;

            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Info = ex.Message;
                _responseDTO.Result = null;
            }
            return _responseDTO;
        }

        [Authorize]
        [HttpPost("createSession")]
        public async Task<APIResponseDTO> CreateSession([FromBody] PaymentGatewayRequestDTO paymentGatewayRequestDTO)
        {
            try
            {
                //Basic configuration for Stripe - The secret key is initialized in program.cs (via app settings)
                var options = new SessionCreateOptions
                {
                    SuccessUrl = paymentGatewayRequestDTO.SuccessUrl,
                    CancelUrl = paymentGatewayRequestDTO.FailureUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                //hack for Indian payment accept
                //Enter 4000 0035 6000 0008 as a Credit Card Number

                if (!string.IsNullOrEmpty(paymentGatewayRequestDTO.PaymentItem.OfferCode) && paymentGatewayRequestDTO.PaymentItem.Discount > 0)
                {
                    var offerDiscounts = new List<SessionDiscountOptions>()
                    {
                        new SessionDiscountOptions()

                        {
                            Coupon = paymentGatewayRequestDTO.PaymentItem.OfferCode
                        }
                    };
                    options.Discounts = offerDiscounts;
                }

                //Adding each booking item into the payment gateway request
                foreach (var item in paymentGatewayRequestDTO.PaymentItem.PaymentItemDetails)
                {
                    var PaymentLineItems = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = Convert.ToDecimal(item.BookingPrice) * 100, //*100 is STRIPE SPECIFIC HACK - Else request fails
                            Currency = "INR",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item?.Listing?.Name,
                                Description = item?.Listing?.Description,
                                Images = new List<string> { item.Listing.ImageUrl },
                            }
                        },
                        Quantity = item.DayOfStay,
                    };

                    options.LineItems.Add(PaymentLineItems);
                }

                //Payment Gateway's session
                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                //Storing the session for further use
                paymentGatewayRequestDTO.SessionUrl = session.Url;

                PaymentItem paymentItem = await _dbContext.PaymentItem.FirstAsync(x => x.BookingItemId == paymentGatewayRequestDTO.PaymentItem.BookingItemId);
                if (paymentItem != null)
                {
                    paymentItem.StripeSessionId = session.Id;
                    paymentItem.PaymentIntentId = session.PaymentIntentId;
                    _dbContext.SaveChanges();
                }

                _responseDTO.HasError = false;
                _responseDTO.Info = "";
                _responseDTO.Result = paymentGatewayRequestDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Info = ex.Message;
                _responseDTO.Result = null;
            }
            return _responseDTO;
        }
    }
}
