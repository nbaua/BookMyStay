using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IPaymentService
    {
        Task<APIResponseDTO> CreatePaymentRequest(BookingDTO bookingDTO);
    }
}
