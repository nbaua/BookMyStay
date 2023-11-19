using BookMyStay.AuthAPI.Models;
using BookMyStay.AuthAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookMyStay.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponseDTO _responseDTO;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDTO = new APIResponseDTO();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {

            if (registerDTO != null)
            {
                var response = await _authService.Register(registerDTO);
                if (response.Token != "Success")
                {
                    _responseDTO.Info = "Error";
                    _responseDTO.HasError = true;
                    return BadRequest(_responseDTO);
                }
                else
                {
                    _responseDTO.Result = response;
                    _responseDTO.Info = "Success";
                    _responseDTO.HasError = false;
                }
            }
            return Ok(_responseDTO);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var response = await _authService.Login(loginDTO);
            if(response.Token != null)
            {
                _responseDTO.Result = response;
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            else { 
                _responseDTO.Result = response;
                _responseDTO.Info = "Error";
                _responseDTO.HasError = true;
            }
            return Ok(_responseDTO);
        }

        [HttpPost]
        [Route("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterDTO registerDTO)
        {
            var response = await _authService.AssignRole(registerDTO);
            if (response)
            {
                _responseDTO.Result = response;
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            else
            {
                _responseDTO.Result = response;
                _responseDTO.Info = "Error";
                _responseDTO.HasError = true;
            }
            return Ok(response);
        }

    }
}
