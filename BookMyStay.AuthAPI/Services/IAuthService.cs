using BookMyStay.AuthAPI.Models;

namespace BookMyStay.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<UserDTO> Register(RegisterDTO registerDTO);
        Task<UserDTO> Login(LoginDTO loginDTO);
        Task<bool> AssignRole(RegisterDTO registerDTO);
    }
}
