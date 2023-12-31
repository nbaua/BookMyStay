using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IServiceBase _service;

        public BookingService(IServiceBase service)
        {
            _service = service;
        }

        public async Task<APIResponseDTO> ManageOfferAsync(BookingDTO offerDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.BookingApiEndPoint + Constants.BookingApiApplyOfferOnBooking,
                Payload = offerDTO,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> DeleteBookingAsync(int bookingItemId)
        {
            return await _service.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.BookingApiEndPoint + Constants.BookingApiDeleteBookingByItemId + $"/{bookingItemId}",
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> DeleteAllBookingsAsync(int bookingDetailsId)
        {
            return await _service.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.BookingApiEndPoint + Constants.BookingApiDeleteBookingsByDetailsId + $"/{bookingDetailsId}",
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> GetBookingsByUserIdAsync(string userId)
        {
            return await _service.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.BookingApiEndPoint + Constants.BookingApiGetBookingsByUserId + $"/{userId}",
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> ManageBookingAsync(BookingDTO bookingDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.BookingApiEndPoint + Constants.BookingApiManageBooking,
                Payload = bookingDTO,
                Token = ""
            });
        }
    }
}
