using AutoMapper;
using BookMyStay.BookingAPI.Data;
using BookMyStay.BookingAPI.Models;
using BookMyStay.BookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.BookingAPI.Controllers
{

    [Route("api/booking")]
    [ApiController]
    public class BookingAPIController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseDTO _responseDTO;
        private IMapper _mapper;
        private IListingService _listingService;
        private IOfferService _offerService;

        public BookingAPIController(ApplicationDBContext ctx, IMapper mapper
            , IListingService listingService, IOfferService offerService)
        {
            _dbContext = ctx;
            _mapper = mapper;
            _responseDTO = new APIResponseDTO();
            _listingService = listingService;
            _offerService = offerService;
        }

        [HttpGet("GetBookings/{UserId}")]
        public async Task<APIResponseDTO> GetBookings(string UserId)
        {
            try
            {
                BookingDTO bookingDTO = new()
                {
                    BookingItemDTO = _mapper.Map<BookingItemDTO>(_dbContext.BookingItems.First(u => u.UserId == UserId)),
                };
                bookingDTO.BookingDetailsDTO = _mapper.Map<IEnumerable<BookingDetailsDTO>>(
                    _dbContext.BookingDetails.Where(u => u.BookingItemId == bookingDTO.BookingItemDTO.BookingItemId)
                    );

                IEnumerable<ListingDTO> BookingListings = await _listingService.GetListings(); //All listings from another ref service


                foreach (var item in bookingDTO.BookingDetailsDTO)
                {
                    //For the listing items prices we need to have inter service communication
                    //If we fail to retrieve these details using the inter service communication, there will be an exception
                    item.Listing = BookingListings.FirstOrDefault(k => k.ListingId == item.ListingId);
                    bookingDTO.BookingItemDTO.BookingTotal += (item.DayOfStay * item.Listing.ListingPrice);
                }

                //Apply offer
                if (!string.IsNullOrEmpty(bookingDTO.BookingItemDTO.OfferCode))
                {
                    OfferDTO offerDTO = await _offerService.GetOfferByCode(bookingDTO.BookingItemDTO.OfferCode);
                    if (offerDTO != null && bookingDTO.BookingItemDTO.BookingTotal > 0)
                    {
                        bookingDTO.BookingItemDTO.Discount = Math.Round(((bookingDTO.BookingItemDTO.BookingTotal * offerDTO.OfferDiscountPerc) / 100), 2);
                        bookingDTO.BookingItemDTO.BookingTotal -= (double)bookingDTO.BookingItemDTO.Discount;
                    }
                }

                _responseDTO.Result = bookingDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Result = ex.Message;
            }
            return _responseDTO;
        }

        [HttpPost("Delete/{BookingItemId}")]
        public async Task<APIResponseDTO> Delete(int BookingItemId)
        {
            try
            {
                BookingDetails bookingDetails = _dbContext.BookingDetails.First(d => d.BookingItemId == BookingItemId);
                int TotalBookings = _dbContext.BookingDetails.Where(d => d.BookingItemId == BookingItemId).Count();

                _dbContext.BookingDetails.Remove(bookingDetails);

                if (TotalBookings == 1)
                {
                    var BookingToBeCleared = await _dbContext.BookingItems.FirstOrDefaultAsync(i => i.BookingItemId == bookingDetails.BookingItemId);
                    if (BookingToBeCleared != null)
                    {
                        _dbContext.BookingItems.Remove(BookingToBeCleared);
                    }
                }

                await _dbContext.SaveChangesAsync();
                _responseDTO.Result = true;
            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Result = ex.Message;
            }
            return _responseDTO;
        }

        [HttpPost("Manage")]
        public async Task<APIResponseDTO> Manage(BookingDTO bookingDTO)
        {
            try
            {
                //1) Check if there is any existing booking for logged in user - if not create a NEW booking
                var BookingItemExists = await _dbContext.BookingItems.AsNoTracking().FirstOrDefaultAsync(i => i.UserId == bookingDTO.BookingItemDTO.UserId);
                if (BookingItemExists == null)
                {
                    //create the booking item and booking detail
                    BookingItem bookingItem = _mapper.Map<BookingItem>(bookingDTO.BookingItemDTO);
                    _dbContext.BookingItems.Add(bookingItem);
                    await _dbContext.SaveChangesAsync();

                    bookingDTO.BookingDetailsDTO.First().BookingItemId = bookingItem.BookingItemId;
                    _dbContext.BookingDetails.Add(_mapper.Map<BookingDetails>(bookingDTO.BookingDetailsDTO.First()));

                    await _dbContext.SaveChangesAsync();

                }
                //2)  Update-insert the booking
                else
                {
                    var BookingDetailExists = await _dbContext.BookingDetails.AsNoTracking().FirstOrDefaultAsync(
                        d => d.ListingId == bookingDTO.BookingDetailsDTO.First().ListingId
                        && d.BookingItemId == BookingItemExists.BookingItemId);

                    // --2.1 - if user has a booking but not for an existing listing, create NEW
                    if (BookingDetailExists == null)
                    {
                        bookingDTO.BookingDetailsDTO.First().BookingItemId = BookingItemExists.BookingItemId;
                        _dbContext.BookingDetails.Add(_mapper.Map<BookingDetails>(bookingDTO.BookingDetailsDTO.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                    else // --2.2 - if user has an existing booking, update the details like no of stay, offer, discount etc
                    {
                        bookingDTO.BookingDetailsDTO.First().DayOfStay += BookingDetailExists.DayOfStay;
                        bookingDTO.BookingDetailsDTO.First().BookingItemId += BookingDetailExists.BookingItemId;
                        bookingDTO.BookingDetailsDTO.First().BookingDetailId += BookingDetailExists.BookingDetailId;
                        //bookingDTO.BookingDetailsDTO.First().ListingId += BookingDetailExists.ListingId;
                        _dbContext.BookingDetails.Update(_mapper.Map<BookingDetails>(bookingDTO.BookingDetailsDTO.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                }

                _responseDTO.Result = bookingDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Result = ex.Message;
            }
            return _responseDTO;
        }

        [HttpPost("ManageOffer")]
        public async Task<object> ManageOffer([FromBody] BookingDTO bookingDTO)
        {
            try
            {
                var bookings = _dbContext.BookingItems.First(i => i.UserId == bookingDTO.BookingItemDTO.UserId);
                bool offerCodeExists = false;
                bool offerCodeEmpty = false;

                if (!string.IsNullOrEmpty(bookingDTO.BookingItemDTO.OfferCode))
                {
                    OfferDTO offerDTO = await _offerService.GetOfferByCode(bookingDTO.BookingItemDTO.OfferCode);
                    offerCodeExists = !string.IsNullOrEmpty(offerDTO.OfferCode);
                }
                offerCodeEmpty = string.IsNullOrEmpty(bookingDTO.BookingItemDTO.OfferCode);

                if ((offerCodeExists || offerCodeEmpty))
                {
                    bookings.OfferCode = bookingDTO.BookingItemDTO.OfferCode;
                    _dbContext.BookingItems.Update(bookings);
                    await _dbContext.SaveChangesAsync();

                    _responseDTO.HasError = false;
                    _responseDTO.Result = true;
                    _responseDTO.Info = "Offer Code Applied";
                }
                else
                {
                    _responseDTO.Result = false;
                    _responseDTO.HasError = true;
                    _responseDTO.Info = "Offer Code Not Applied";
                }

            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Result = false;
                _responseDTO.Info = ex.Message;
            }
            return _responseDTO;
        }

    }

}

