using BookMyStay.BookingAPI.Models;
using Newtonsoft.Json;

namespace BookMyStay.BookingAPI.Services
{
    public class OfferService: IOfferService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OfferService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<OfferDTO> GetOfferByCode(string offerCode)
        {
            var client = _httpClientFactory.CreateClient("Offer");
            var response = await client.GetAsync($"/api/offer/code/" + offerCode);
            var content = await response.Content.ReadAsStringAsync();
            var parsedString = JsonConvert.DeserializeObject<APIResponseDTO>(content);

            if (parsedString != null && parsedString.HasError == false)
            {
                return JsonConvert.DeserializeObject<OfferDTO>(Convert.ToString(parsedString.Result));
            }

            return new OfferDTO();
        }
    }
}
