using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public class OfferService : IOfferService
    {
        private readonly IServiceBase _service;

        public OfferService(IServiceBase service) { 
            _service = service;
        }

        public async Task<APIResponseDTO> CreateOfferAsync(OfferDTO offerDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.OfferApiEndPoint + Constants.OfferApiCreateGetAllOffers,
                Payload = offerDTO,
                Token = ""
            }); ;
        }

        public async Task<APIResponseDTO> UpdateOfferAsync(OfferDTO offerDTO)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "PUT",
                RequestUrl = Constants.OfferApiEndPoint + Constants.OfferApiCreateGetAllOffers,
                Payload = offerDTO,
                Token = ""
            }); ;
        }

        public async Task<APIResponseDTO> GetAllOffersAsync()
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.OfferApiEndPoint + Constants.OfferApiCreateGetAllOffers,
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> GetOfferByCodeAsync(string code)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.OfferApiEndPoint +  Constants.OfferApiGetOfferByCode + "/" + code,
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> GetOfferByIdAsync(int id)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.OfferApiEndPoint + Constants.OfferApiGetOrDeleteOfferById + "/" + id,
                Payload = string.Empty,
                Token = ""
            });
        }

        public async Task<APIResponseDTO> DeleteOfferAsync(int id)
        {
            return await _service.SendRequestAsync(new APIRequestDTO() //.SendRequestAsync<APIResponseDTO>(new APIRequestDTO()
            {
                RequestType = "DELETE",
                RequestUrl = Constants.OfferApiEndPoint + Constants.OfferApiGetOrDeleteOfferById + "/" + id,
                Payload = string.Empty, //to-do
                Token = ""
            }); ;
        }
    }
}
