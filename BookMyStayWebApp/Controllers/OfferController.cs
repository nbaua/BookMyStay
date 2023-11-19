using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookMyStay.WebApp.Controllers
{
    [Route("Offer")]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        public async Task<IActionResult> Index()
        {
            List<OfferDTO> offersList = [];//simplified as versus new() or new List<OfferDTO>()[]

            APIResponseDTO response = await _offerService.GetAllOffersAsync();
            if (response != null && !response.HasError)
            {
                offersList = JsonConvert.DeserializeObject<List<OfferDTO>>(Convert.ToString(response.Result));
                return View(offersList);
            }
            else
            {
                TempData["Error"] = response.Info;
                return View(offersList);
            }
        }

        [Route("manage/{id:int}")]
        public async Task<IActionResult> Manage(int id)
        {
            OfferDTO offer = new();

            APIResponseDTO response = await _offerService.GetOfferByIdAsync(id);
            if (response != null && !response.HasError)
            {
                offer = JsonConvert.DeserializeObject<OfferDTO>(Convert.ToString(response.Result));
            }
            return View(offer);
        }

        [HttpPost]
        [Route("manage/{id:int}")]
        public async Task<IActionResult> Manage(OfferDTO offerDTO)
        {
            if (ModelState.IsValid)
            {
                APIResponseDTO response;
                if (offerDTO.OfferId == 0) //use instead of id to prevent URL tempering
                {
                    response = await _offerService.CreateOfferAsync(offerDTO);
                }
                else
                {
                    response = await _offerService.UpdateOfferAsync(offerDTO);
                }

                if (response.Result != null && !response.HasError)
                {
                    TempData["Success"] = offerDTO.OfferId == 0 ? Constants.OfferCreated : Constants.OfferUpdated;
                }
                else
                {
                    TempData["Error"] = $"Error: {response.Info}";
                }
                return RedirectToAction("Index");
            }
            return View(offerDTO);
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                APIResponseDTO response = await _offerService.DeleteOfferAsync(id);
                if (response.Result != null && !response.HasError)
                {
                    TempData["Success"] = Constants.OfferDeleted;
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
