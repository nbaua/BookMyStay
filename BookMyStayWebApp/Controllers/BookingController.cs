using BookMyStay.WebApp.Models;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace BookMyStay.WebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _BookingService;
        public BookingController(IBookingService BookingService)
        {
            _BookingService = BookingService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            APIResponseDTO response = await _BookingService.GetBookingsByUserIdAsync(userId);
            if (response != null && response.HasError == false)
            {
                BookingDTO bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(Convert.ToString(response.Result));
                return View(bookingDTO);
            }
            else
            {
                return View(new BookingDTO());
            }
         }
    }
}
