using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookMyStay.WebApp.Controllers
{
    [Route("Listing")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
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

        [Route("manage/{id:int}")]
        public async Task<IActionResult> Manage(int id)
        {
            ListingDTO listing = new();

            APIResponseDTO response = await _listingService.GetListingByIdAsync(id);
            if (response != null && !response.HasError)
            {
                listing = JsonConvert.DeserializeObject<ListingDTO>(Convert.ToString(response.Result));
            }
            return View(listing);
        }

        [HttpPost]
        [Route("manage/{id:int}")]
        public async Task<IActionResult> Manage(ListingDTO listingDTO)
        {
            if (ModelState.IsValid)
            {
                APIResponseDTO response;
                if (listingDTO.ListingId == 0)
                {
                    response = await _listingService.CreateListingAsync(listingDTO);
                }
                else
                {
                    response = await _listingService.UpdateListingAsync(listingDTO);
                }

                if (response.Result != null && !response.HasError)
                {
                    TempData["Success"] = listingDTO.ListingId == 0 ? Constants.ListingCreated : Constants.ListingUpdated;
                }
                else
                {
                    TempData["Error"] = $"Error: {response.Info}";
                }
                return RedirectToAction("Index");
            }
            return View(listingDTO);
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                APIResponseDTO response = await _listingService.DeleteListingAsync(id);
                if (response.Result != null && !response.HasError)
                {
                    TempData["Success"] = Constants.ListingDeleted;
                }
                else
                {
                    TempData["Error"] = $"Error: {response.Info}";
                }
                return RedirectToAction("Index"); ;
            }
            return View("Index", "Home");
        }
    }
}
