using AutoMapper;
using BookMyStay.PaymentAPI.Models;

namespace BookMyStay.PaymentAPI.Data
{
    public class DataMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<BookingItem, BookingItemDTO>().ReverseMap();
                //config.CreateMap<BookingDetails, BookingDetailsDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
