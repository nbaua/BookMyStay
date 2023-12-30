using AutoMapper;
using BookMyStay.OfferAPI.Models;

namespace BookMyStay.OfferAPI.Data
{
    public class DataMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OfferDTO, Offer>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
