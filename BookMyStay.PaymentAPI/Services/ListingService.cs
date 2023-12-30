using BookMyStay.PaymentAPI.Models;
using Newtonsoft.Json;

namespace BookMyStay.PaymentAPI.Services
{
    public class ListingService : IListingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ListingService(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ListingDTO>> GetListings()
        {
           var client =  _httpClientFactory.CreateClient("Listing");
            var response = await client.GetAsync($"/api/listing");
            var content = await response.Content.ReadAsStringAsync();
            var parsedString = JsonConvert.DeserializeObject<APIResponseDTO>(content);
            
            if(parsedString != null && parsedString.HasError == false) {
                return JsonConvert.DeserializeObject<IEnumerable<ListingDTO>>(Convert.ToString(parsedString.Result));
            }

            return new List<ListingDTO>();
        }
    }
}
