using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IPaymentService
    {
        Task<APIResponseDTO> CreatePaymentRequest(BookingDTO bookingDTO); 
        Task<APIResponseDTO> CreatePaymentSession(PaymentGatewayRequestDTO paymentGatewayRequestDTO); 
        Task<APIResponseDTO> ValidatePaymentSession(int BookingItemId); 
    }
}
