
using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public class DBLoggerService : IDBLoggerService
    {
        private readonly IServiceBase _service;
        public DBLoggerService(IServiceBase service)
        {
            _service = service;
        }
        public Task<APIResponseDTO> LogToDB(string queueName)
        {
            return _service.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "GET",
                RequestUrl = Constants.DBLoggerApiEndPoint + Constants.DBLoggerApiLogQueue + "/" + queueName,
                Payload = "",
                Token = ""
            });
        }
    }
}
