using BookMyStay.WebApp.Helpers;
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

        public async Task<IActionResult> Delete(int id) {
            //var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            APIResponseDTO response = await _BookingService.DeleteBookingAsync(id);
            if (response != null && response.HasError == false)
            {
                TempData["Success"] = Constants.BookingRemoved;
                return RedirectToAction("Index", "Booking");
            }
            else
            {
                TempData["Error"] = response.Info;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ManageOffer(BookingDTO bookingDTO)
        {
            APIResponseDTO response = await _BookingService.ManageOfferAsync(bookingDTO);
            if (response != null && response.HasError == false)
            {
                TempData["Success"] = Constants.OfferCodeApplied;
                return RedirectToAction("Index", "Booking");
            }
            else
            {
                TempData["Error"] = response.Info;
                return RedirectToAction("Index", "Booking");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOffer(BookingDTO bookingDTO)
        {
            bookingDTO.BookingItemDTO.OfferCode = "";
            APIResponseDTO response = await _BookingService.ManageOfferAsync(bookingDTO);
            if (response != null && response.HasError == false)
            {
                TempData["Success"] = Constants.OfferCodeRemoved;
                return RedirectToAction("Index", "Booking");
            }
            else
            {
                TempData["Error"] = response.Info;
                return RedirectToAction("Index", "Booking");
            }
        }

    }
}
