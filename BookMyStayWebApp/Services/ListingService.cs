using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public class ListingService : IListingService
    {
        private readonly IServiceBase _service;

        public ListingService(IServiceBase service)
        {
            _service = service;
        }

        public async Task<APIResponseDTO> CreateListingAsync(ListingDTO ListingDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.ListingApiEndPoint + Constants.ListingApiCreateGetAllListings,
                Payload = ListingDTO,
                Token = ""
            }); ;
        }

        public async Task<APIResponseDTO> GetAllListingsAsync()
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.ListingApiEndPoint + Constants.ListingApiCreateGetAllListings,
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> GetListingByIdAsync(int id)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.ListingApiEndPoint + Constants.ListingApiGetOrDeleteListingById + "/" + id,
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> UpdateListingAsync(ListingDTO ListingDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "PUT",
                RequestUrl = Constants.ListingApiEndPoint + Constants.ListingApiCreateGetAllListings,
                Payload = ListingDTO,
                Token = ""
            }); ;
        }
        public async Task<APIResponseDTO> DeleteListingAsync(int id)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "DELETE",
                RequestUrl = Constants.ListingApiEndPoint + Constants.ListingApiGetOrDeleteListingById + "/" + id,
                Payload = string.Empty, //to-do
                Token = ""
            }); ;
        }
    }
}
