using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IListingService
    {
        Task<APIResponseDTO> CreateListingAsync(ListingDTO ListingDTO);
        Task<APIResponseDTO> UpdateListingAsync(ListingDTO ListingDTO);
        Task<APIResponseDTO> DeleteListingAsync(int id);
        Task<APIResponseDTO> GetAllListingsAsync();
        Task<APIResponseDTO> GetListingByIdAsync(int id);
    }
}
