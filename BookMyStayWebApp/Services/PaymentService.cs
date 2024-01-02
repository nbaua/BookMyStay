using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IServiceBase _service;

        public PaymentService(IServiceBase service)
        {
            _service = service;
        }

        public async Task<APIResponseDTO> CreatePaymentRequest(BookingDTO bookingDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.PaymentApiEndPoint + Constants.PaymentApiCreatePaymentRequest,
                Payload = bookingDTO,
                Token = ""
            }); ;
        } 
        
        public async Task<APIResponseDTO> CreatePaymentSession(PaymentGatewayRequestDTO paymentGatewayRequestDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.PaymentApiEndPoint + Constants.PaymentApiCreateSessionRequest,
                Payload = paymentGatewayRequestDTO,
                Token = ""
            }); ;
        }

        public async Task<APIResponseDTO> ValidatePaymentSession(int BookingItemId)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.PaymentApiEndPoint + Constants.PaymentApiValidateSessionRequest,
                Payload = BookingItemId,
                Token = ""
            }); ;
        }
    }
}
