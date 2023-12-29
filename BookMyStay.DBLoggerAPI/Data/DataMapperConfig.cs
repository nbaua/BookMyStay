using AutoMapper;
using BookMyStay.DBLoggerAPI.Models;

namespace BookMyStay.DBLoggerAPI.Data
{
    public class DataMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<DBLoggerLog, DBLoggerLogDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
