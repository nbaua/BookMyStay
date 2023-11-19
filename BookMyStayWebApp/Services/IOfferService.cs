using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IOfferService
    {
        Task<APIResponseDTO> CreateOfferAsync(OfferDTO offerDTO);
        Task<APIResponseDTO> UpdateOfferAsync(OfferDTO offerDTO);
        Task<APIResponseDTO> DeleteOfferAsync(int id);
        Task<APIResponseDTO> GetAllOffersAsync();
        Task<APIResponseDTO> GetOfferByIdAsync(int id);
        Task<APIResponseDTO> GetOfferByCodeAsync(string code); 
    }
}
