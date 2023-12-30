using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace BookMyStay.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IBookingService _bookingService;

        public HomeController(IListingService listingService, IBookingService bookingService)
        {
            _listingService = listingService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            List<ListingDTO> listingsList = [];//simplified as versus new() or new List<ListingDTO>()[]

            APIResponseDTO response = await _listingService.GetAllListingsAsync();
            if (response != null && !response.HasError)
            {
                listingsList = JsonConvert.DeserializeObject<List<ListingDTO>>(Convert.ToString(response.Result));
                return View(listingsList);
            }
            else
            {
                TempData["Error"] = response.Info;
                return View(listingsList);
            }
        }

        [Route("{id:int}")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            ListingDTO listingsList = new();

            APIResponseDTO response = await _listingService.GetListingByIdAsync(id);
            if (response != null && !response.HasError)
            {
                listingsList = JsonConvert.DeserializeObject<ListingDTO>(Convert.ToString(response.Result));
                return View(listingsList);
            }
            else
            {
                TempData["Error"] = response.Info;
                return View(listingsList);
            }
        }


        [Authorize]
        [HttpPost]
        [Route("{id:int}")]
        [ActionName("Detail")]
        public async Task<IActionResult> Detail(ListingDTO listingDTO)
        {
            BookingItemDTO bookingItem = new BookingItemDTO()
            {
                UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value,

            };

            BookingDetailsDTO bookingDetails = new BookingDetailsDTO()
            {
                BookingItemId = bookingItem.BookingItemId,
                ListingId = listingDTO.ListingId,
                DayOfStay = listingDTO.BookingDays,
                BookingItemDTO = bookingItem
            };

            BookingDTO bookingDTO = new BookingDTO();
            bookingDTO.BookingItemDTO = bookingItem;
            bookingDTO.BookingDetailsDTO = new List<BookingDetailsDTO>() { bookingDetails };


            APIResponseDTO response = await _bookingService.ManageBookingAsync(bookingDTO);
            if (response != null && !response.HasError)
            {
                TempData["Success"] = Constants.BookingCreated;
                return RedirectToAction("", "Home");
            }
            else
            {
                TempData["Error"] = response.Info;
                return View(bookingDTO);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
