using BookMyStay.PaymentAPI.Models;

namespace BookMyStay.PaymentAPI.Services
{
    public interface IListingService
    {
        Task<IEnumerable<ListingDTO>> GetListings();
    }
}
