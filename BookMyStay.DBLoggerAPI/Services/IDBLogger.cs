using BookMyStay.DBLoggerAPI.Models;

namespace BookMyStay.DBLoggerAPI.Services
{
    public interface IDBLogger
    {
        public Task<APIResponseDTO> ConsumeMQAndLogToDB(string queueName);
    }
}

