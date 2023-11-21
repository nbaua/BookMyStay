using AutoMapper;
using BookMyStay.BookingAPI.Models;

namespace BookMyStay.BookingAPI.Data
{
    public class DataMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BookingItem, BookingItemDTO>().ReverseMap();
                config.CreateMap<BookingDetails, BookingDetailsDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
