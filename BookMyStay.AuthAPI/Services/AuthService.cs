using AutoMapper;
using BookMyStay.AuthAPI.Data;
using BookMyStay.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BookMyStay.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(ApplicationDBContext ctx, UserManager<IdentityUser> uMgr, RoleManager<IdentityRole> rMgr, ITokenGenerator tokenGenerator)
        {
            _dbContext = ctx;
            _userManager = uMgr;
            _roleManager = rMgr;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<bool> AssignRole(RegisterDTO registerDTO)
        {
            var role = "User";

            if (registerDTO.Email != null && registerDTO.Email == "admin@bms.com")
            {
                role = "Admin";
            }
            else
            {
                role = "User";
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == registerDTO.UserName.ToLower());
            if (user != null)
            {
                //If role does not exist - create it - NOT SAFE FOR PROD env - Just for demo purpose.
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }

        public async Task<UserDTO> Login(LoginDTO loginDTO)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName.ToLower() == loginDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (user == null || isValid == false)
            {
                return new UserDTO { UserName = "", Token = "" };
            }
            else
            {
                var role = await _userManager.GetRolesAsync(user);
                var token = _tokenGenerator.GenerateToken(user,role.FirstOrDefault());
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = token,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
            }
        }

        public async Task<UserDTO> Register(RegisterDTO registerDTO)
        {
            var response = new UserDTO();

            IdentityUser user = new IdentityUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                NormalizedEmail = registerDTO.Email.ToUpper(),
                NormalizedUserName = registerDTO.UserName.ToUpper(),
                PhoneNumber = registerDTO.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded)
                {
                    response.UserName = registerDTO.UserName;
                    response.Email = registerDTO.Email;
                    response.PhoneNumber = registerDTO.PhoneNumber;
                    response.Token = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Token = ex.Message;
                //to-do -log exception messages
            }
            return response;
        }
    }
}
