using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IBookingService
    {
        Task<APIResponseDTO> GetBookingsByUserIdAsync(string userId);
        Task<APIResponseDTO> ManageBookingAsync(BookingDTO bookingDTO);
        Task<APIResponseDTO> ManageOfferAsync(BookingDTO bookingDTO);
        Task<APIResponseDTO> DeleteBookingAsync(int bookingDetailId);
    }
}
