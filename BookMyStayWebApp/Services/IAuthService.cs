using BookMyStay.WebApp.Models;

namespace BookMyStay.WebApp.Services
{
    public interface IAuthService
    {
        Task<APIResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<APIResponseDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<APIResponseDTO> AssignRoleAsync(RegisterDTO registerDTO);
    }
}
