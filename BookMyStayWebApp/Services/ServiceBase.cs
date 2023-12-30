using BookMyStay.WebApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace BookMyStay.WebApp.Services
{
    public class ServiceBase : IServiceBase
    {
        private IHttpClientFactory _httpClientFactory;
        private ITokenHandler _tokenHandler;
        public ServiceBase(IHttpClientFactory httpClientFactory, ITokenHandler tokenHandler)
        {
            _httpClientFactory = httpClientFactory;
            _tokenHandler = tokenHandler;
        }

        //Task<T> SendRequestAsync<T>(APIRequestDTO aPIRequestDTO)
        //public async Task<T> IServiceBase.SendRequestAsync<T>(APIRequestDTO requestDTO)
        public async Task<APIResponseDTO?> SendRequestAsync(APIRequestDTO requestDTO, bool withToken = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("BMSClient");

                HttpRequestMessage message = new();
                message.Method = new HttpMethod(requestDTO.RequestType);
                message.Headers.TryAddWithoutValidation("Content-Type", "application/json");

                //message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (withToken)
                {
                    var token = _tokenHandler.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDTO.RequestUrl);

                if (requestDTO.Payload != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Payload), Encoding.UTF8, "application/json");
                }

                var response = await client.SendAsync(message);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new APIResponseDTO() { HasError = true, Info = "Not Found", Result = null };
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return new APIResponseDTO() { HasError = true, Info = "Forbidden", Result = null };
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new APIResponseDTO() { HasError = true, Info = "Unauthorized", Result = null };
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return new APIResponseDTO() { HasError = true, Info = "Server Error", Result = null };
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        return JsonConvert.DeserializeObject<APIResponseDTO>(result);
                        //return new APIResponseDTO() { HasError = false, Info = "Success", Result = apiResponseDTO };
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                return new APIResponseDTO() { HasError = true, Info = "Exception", Result = ex.Message };
            }

        }
    }
}
