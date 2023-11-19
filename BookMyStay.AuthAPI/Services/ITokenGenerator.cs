using Microsoft.AspNetCore.Identity;

namespace BookMyStay.AuthAPI.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(IdentityUser identityUser, string? role);
    }
}
