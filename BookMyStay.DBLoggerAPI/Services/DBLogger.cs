using AutoMapper;
using Azure;
using BookMyStay.DBLoggerAPI.Data;
using BookMyStay.DBLoggerAPI.Models;
using BookMyStay.MessageBroker;
using Newtonsoft.Json;

namespace BookMyStay.DBLoggerAPI.Services
{
    public class DBLogger : IDBLogger
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseDTO _responseDTO;
        private readonly IMessageHandler _MessageHandler;
        private IMapper _mapper;
        private DBLoggerLogDTO LoggerLogDTO;

        public DBLogger(IMessageHandler MessageHandler, ApplicationDBContext ctx, IMapper mapper)
        {
            _dbContext = ctx;
            _mapper = mapper;
            _responseDTO = new APIResponseDTO();
            _MessageHandler = MessageHandler;
            LoggerLogDTO = new DBLoggerLogDTO();
        }
        public async Task<APIResponseDTO> ConsumeMQAndLogToDB(string queueName)
        {
            var message = await _MessageHandler.ConsumeMessage(queueName);
            if (message == null)
            {
                return new APIResponseDTO{ HasError = true, Info="No messages found or error occurred, check logs", Result = "" };
            }
            else
            {
                LoggerLogDTO.Payload = message;
                LoggerLogDTO.QueueName = queueName;
                LoggerLogDTO.UserId = "";

                try
                {
                    BookingDTO bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(Convert.ToString(message));
                    if (bookingDTO != null) {
                        LoggerLogDTO.UserId = bookingDTO.BookingItemDTO.UserId;
                    }
                }
                catch (Exception Ex)
                {
                    //gulp-it for now
                    Ex.Message.ToString();
                }


                DBLoggerLogDTO LoggerLog = _mapper.Map<DBLoggerLogDTO>(LoggerLogDTO);
                _dbContext.DBLoggers.Add(LoggerLog);
                _dbContext.SaveChanges();
                
                Console.WriteLine(message); //to-do save messages to tge database

                return new APIResponseDTO { HasError = false, Info = "", Result = message }; ;
            }
        }
    }
}
