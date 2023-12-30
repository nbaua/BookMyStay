using AutoMapper;
using BookMyStay.ListingAPI.Models;

namespace BookMyStay.ListingAPI.Data
{
    public class DataMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ListingDTO, Listing>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
