using BookMyStay.BookingAPI.Models;

namespace BookMyStay.BookingAPI.Services
{
    public interface IOfferService
    {
        Task<OfferDTO> GetOfferByCode(string offerCode);

    }
}
