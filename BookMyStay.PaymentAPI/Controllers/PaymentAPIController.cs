using AutoMapper;
using BookMyStay.PaymentAPI.Data;
using BookMyStay.PaymentAPI.Models;
using BookMyStay.PaymentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

                paymentItemDTO.PaymentItemDetails = _mapper.Map<IEnumerable<PaymentItemDetailsDTO>>(bookingDTO.BookingDetailsDTO);

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
    }
}
