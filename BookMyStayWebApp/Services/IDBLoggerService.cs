using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IDBLoggerService
    {
        Task<APIResponseDTO> LogToDB(string queueName);
    }
}
