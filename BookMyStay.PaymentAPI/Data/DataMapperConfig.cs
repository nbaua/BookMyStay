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
                config.CreateMap<PaymentItemDTO, BookingItemDTO>().ReverseMap();

                config.CreateMap<BookingDetailsDTO, PaymentItemDetailsDTO>().
                ForMember(x=>x.BookingPrice, y=> y.MapFrom(z => z.Listing.ListingPrice));

                config.CreateMap<PaymentItemDetailsDTO, BookingDetailsDTO>();

                config.CreateMap<PaymentItem, PaymentItemDTO>().ReverseMap();
                config.CreateMap<PaymentItemDetails, PaymentItemDetailsDTO>().ReverseMap();

            });

            return mappingConfig;
        }
    }
}
