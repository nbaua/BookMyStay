using BookMyStay.MessageBroker;
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
        private readonly IPaymentService _PaymentService;
        private readonly IMessageHandler _MessageHandler;
        private readonly IDBLoggerService _DBLoggerService;

        public BookingController(IBookingService BookingService,
            IPaymentService PaymentService,
            IMessageHandler MessageHandler,
            IDBLoggerService DBLoggerService
            )
        {
            _BookingService = BookingService;
            _PaymentService = PaymentService;
            _MessageHandler = MessageHandler;
            _DBLoggerService = DBLoggerService;
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

        [Authorize] //Get the BookingDto and send the message to RabbitMQ
        public async Task<IActionResult> CheckOut()
        {

            //publish the message to RabbitMq
            var name = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Name)?.FirstOrDefault()?.Value;
            var email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            var userName = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;

            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            APIResponseDTO response = await _BookingService.GetBookingsByUserIdAsync(userId);
            if (response != null && response.HasError == false)
            {
                BookingDTO bookingDTO = JsonConvert.DeserializeObject<BookingDTO>(Convert.ToString(response.Result));
                bookingDTO.BookingItemDTO.Name = name;
                bookingDTO.BookingItemDTO.Email = email;

                await _MessageHandler.PublishMessage(Constants.BrokerCheckoutQueue, bookingDTO);

                return View(bookingDTO);
            }
            else
            {
                return View(new BookingDTO());
            }
        }

        [Authorize] //Post the BookingDto and process the message
        [HttpPost]
        public async Task<IActionResult> CheckOut(BookingDTO bookingDTO)
        {
            //Step 1: save the published RabbitMq message to database, for future reporting
            var name = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Name)?.FirstOrDefault()?.Value;
            var email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            var userName = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;

            APIResponseDTO response = await _DBLoggerService.LogToDB(Constants.BrokerCheckoutQueue);

            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            response = new APIResponseDTO();
            response = await _BookingService.GetBookingsByUserIdAsync(userId);
            if (response != null && response.HasError == false)
            {
                BookingDTO bDTO = JsonConvert.DeserializeObject<BookingDTO>(response.Result.ToString());
                bookingDTO.BookingDetailsDTO = bDTO.BookingDetailsDTO;
            }

            response = new APIResponseDTO();
            response = await _PaymentService.CreatePaymentRequest(bookingDTO);

            if (response != null && response.HasError == false)
            {
                PaymentItemDTO paymentItemDTO = JsonConvert.DeserializeObject<PaymentItemDTO>(response.Result.ToString());
            }

            return View(bookingDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            APIResponseDTO response = await _BookingService.DeleteBookingAsync(id);
            if (response != null && response.HasError == false)
            {
                TempData["Success"] = Constants.BookingRemoved;
                return RedirectToAction("Index", "Booking");
            }
            else
            {
                TempData["Error"] = response?.Info;
                return View();
            }
        }
        public async Task<IActionResult> DeleteAll(int id)
        {
            //var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            APIResponseDTO response = await _BookingService.DeleteAllBookingsAsync(id);
            if (response != null && response.HasError == false)
            {
                TempData["Success"] = Constants.AllBookingRemoved;
                return RedirectToAction("Index", "Booking");
            }
            else
            {
                TempData["Error"] = response?.Info;
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
