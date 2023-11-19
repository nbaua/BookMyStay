using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IServiceBase _ServiceBase;

        public AuthService(IServiceBase serviceBase)
        {
            _ServiceBase = serviceBase;
        }

        public async Task<APIResponseDTO> AssignRoleAsync(RegisterDTO registerDTO)
        {
            return await _ServiceBase.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.AuthApiEndPoint + Constants.AuthAPIAssignRoleRoute,
                Payload = registerDTO,
                Token = string.Empty,
            },withToken:true);
        }

        public async Task<APIResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            return await _ServiceBase.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.AuthApiEndPoint + Constants.AuthAPILoginRoute,
                Payload = loginDTO,
                Token= string.Empty,
            }, withToken: false);
        }

        public async Task<APIResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            return await _ServiceBase.SendRequestAsync(new APIRequestDTO()
            {
                RequestType = "POST",
                RequestUrl = Constants.AuthApiEndPoint + Constants.AuthAPIRegisterRoute,
                Payload = registerDTO,
                Token = string.Empty,
            }, withToken: false);
        }
    }
}
