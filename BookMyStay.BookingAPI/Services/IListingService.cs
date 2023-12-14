using BookMyStay.BookingAPI.Models;

namespace BookMyStay.BookingAPI.Services
{
    public interface IListingService
    {
        Task<IEnumerable<ListingDTO>> GetListings();
    }
}
