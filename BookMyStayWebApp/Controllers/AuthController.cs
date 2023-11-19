using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookMyStay.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(IAuthService authService, ITokenHandler tokenHandler)
        {
            _authService = authService;
            _tokenHandler = tokenHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginDTO loginDTO = new LoginDTO();
            return View(loginDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            APIResponseDTO response = await _authService.LoginAsync(loginDto);

            if (response != null && response.Result != null)
            {
                UserDTO result = JsonConvert.DeserializeObject<UserDTO>(response.Result.ToString());
                if (!string.IsNullOrEmpty(result.UserName))
                {
                    TempData["Success"] = Constants.UserLoggedIn;

                    await SetSignedInUserIdentity(result);
                    _tokenHandler.SetToken(result.Token);
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = Constants.UserNotLoggedIn;
            }
            else if (response != null && response.HasError)
            {
                TempData["Error"] = response.Info;
                return View(response);
            }
            return View(loginDto);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            APIResponseDTO response = await _authService.RegisterAsync(registerDto);

            if (response != null && response.HasError == false)
            {
                var roleAssign = await _authService.AssignRoleAsync(registerDto);
                if (roleAssign != null)
                {
                    TempData["Success"] = Constants.UserRegistered;
                    return RedirectToAction("Login");
                }
            }

            return View(registerDto);
        }

        public async Task<IActionResult> Logout()
        {
            //APIRequestDTO apiRequestDTO = new();
            await HttpContext.SignOutAsync();
            _tokenHandler.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SetSignedInUserIdentity(UserDTO userDTO) {

            var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(userDTO.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sid, jwtObject.Claims.FirstOrDefault(x=> x.Type == JwtRegisteredClaimNames.Sid).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwtObject.Claims.FirstOrDefault(x=> x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwtObject.Claims.FirstOrDefault(x=> x.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwtObject.Claims.FirstOrDefault(x => x.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
        }
    }
}
