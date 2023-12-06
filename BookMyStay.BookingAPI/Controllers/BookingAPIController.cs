using AutoMapper;
using BookMyStay.BookingAPI.Data;
using BookMyStay.BookingAPI.Models;
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

        public BookingAPIController(ApplicationDBContext ctx, IMapper mapper)
        {
            _dbContext = ctx;
            _mapper = mapper;
            _responseDTO = new APIResponseDTO();
        }

        [HttpPost("Manage")]
        public async Task<APIResponseDTO> Manage(BookingDTO bookingDTO)
        {
            try { 
            
                var BookingItem = await _dbContext.BookingItems.AsNoTracking().FirstOrDefaultAsync( i => i.UserId == bookingDTO.BookingItemDTO.UserId);
                if(BookingItem == null)
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
            catch (Exception ex) {
                _responseDTO.HasError = true;
                _responseDTO.Result = ex.Message;
            }
            return _responseDTO;
        }
    }
}
