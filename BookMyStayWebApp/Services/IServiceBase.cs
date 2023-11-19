using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IServiceBase
    {
        public Task<APIResponseDTO> SendRequestAsync(APIRequestDTO aPIRequestDTO, bool withToken = true);
    }
}
