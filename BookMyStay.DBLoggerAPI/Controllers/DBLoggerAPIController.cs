using BookMyStay.DBLoggerAPI.Data;
using BookMyStay.DBLoggerAPI.Models;
using BookMyStay.DBLoggerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BookMyStay.DBLoggerAPI.Controllers
{

    [Route("api/logger")]
    [ApiController]
    public class DBLoggerAPIController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IDBLogger _DBLogger;
        private DBLoggerLogDTO DBLoggerDTO;

        public DBLoggerAPIController(ApplicationDBContext ctx, IDBLogger DBLogger)
        {
            _dbContext = ctx;
            _DBLogger = DBLogger;
        }

        [HttpGet("LogQueue/{MessageQueueName}")]
        public async Task<APIResponseDTO> LogQueue(string MessageQueueName)
        {
            return await _DBLogger.ConsumeMQAndLogToDB(MessageQueueName);
        }
    }
}
