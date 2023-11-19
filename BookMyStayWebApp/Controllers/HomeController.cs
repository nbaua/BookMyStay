using BookMyStay.WebApp.Models;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookMyStay.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;

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
        public HomeController(IListingService listingService)
        {
            _listingService = listingService;
        }


        [Route("{id:int}")]
        [Authorize]
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
