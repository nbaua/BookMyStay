using AutoMapper;
using BookMyStay.BookingAPI.Data;
using BookMyStay.BookingAPI.Models;
using BookMyStay.BookingAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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
                if(!string.IsNullOrEmpty(bookingDTO.BookingItemDTO.OfferCode))
                {
                    OfferDTO offerDTO = await _offerService.GetOfferByCode(bookingDTO.BookingItemDTO.OfferCode);
                    if(offerDTO != null && bookingDTO.BookingItemDTO.BookingTotal > 0) {
                        bookingDTO.BookingItemDTO.Discount = Math.Round(((bookingDTO.BookingItemDTO.BookingTotal * offerDTO.OfferDiscountPerc) / 100), 2);
                        bookingDTO.BookingItemDTO.BookingTotal -= (double) bookingDTO.BookingItemDTO.Discount ;
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

        [HttpPost("Delete")]
        public async Task<APIResponseDTO> Delete([FromBody] int BookingDetailId)
        {
            try
            {
                BookingDetails bookingDetails = _dbContext.BookingDetails.First(d => d.BookingDetailId == BookingDetailId);
                int TotalBookings = _dbContext.BookingDetails.Where(d => d.BookingDetailId == BookingDetailId).Count();

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

                var BookingItem = await _dbContext.BookingItems.AsNoTracking().FirstOrDefaultAsync(i => i.UserId == bookingDTO.BookingItemDTO.UserId);
                if (BookingItem == null)
                {
                    //Add new booking item along with details
                    //1. New Booking item
                    BookingItem NewBookingItem = _mapper.Map<BookingItem>(bookingDTO.BookingItemDTO);
                    _dbContext.BookingItems.Add(NewBookingItem);
                    await _dbContext.SaveChangesAsync();
                    //2. Add New Booking item ref to details
                    bookingDTO.BookingDetailsDTO.First().BookingItemId = NewBookingItem.BookingItemId;
                    _dbContext.BookingDetails.Add(_mapper.Map<BookingDetails>(bookingDTO.BookingDetailsDTO.First()));
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    //Booking item exists and have same booking details
                    var BookingDetail = await _dbContext.BookingDetails.AsNoTracking().FirstOrDefaultAsync(d => d.BookingItemId == bookingDTO.BookingDetailsDTO.First().BookingItemId
                    && d.BookingItemId == BookingItem.BookingItemId);

                    if (BookingDetail == null)
                    {
                        //Add new booking details
                        bookingDTO.BookingDetailsDTO.First().BookingItemId = BookingItem.BookingItemId;
                        _dbContext.BookingDetails.Add(_mapper.Map<BookingDetails>(bookingDTO.BookingDetailsDTO.First()));
                        await _dbContext.SaveChangesAsync();

                    }
                    else
                    {
                        //update existing booking details
                        bookingDTO.BookingDetailsDTO.First().DayOfStay += BookingDetail.DayOfStay;
                        bookingDTO.BookingDetailsDTO.First().BookingItemId = BookingDetail.BookingItemId;
                        bookingDTO.BookingDetailsDTO.First().BookingDetailId = BookingDetail.BookingDetailId;
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
                bookings.OfferCode = bookingDTO.BookingItemDTO.OfferCode;
                _dbContext.BookingItems.Update(bookings);
                await _dbContext.SaveChangesAsync();
                
                _responseDTO.HasError = false;
                _responseDTO.Result = true;
            }
            catch (Exception ex)
            {
                _responseDTO.HasError = true;
                _responseDTO.Result = ex.Message;
            }
            return _responseDTO;
        }

    }

}

