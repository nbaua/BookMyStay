using BookMyStay.AuthAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookMyStay.AuthAPI.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JWTConfig _jwtConfig;
        public TokenGenerator(IOptions<JWTConfig> jwtConfig) {
            _jwtConfig = jwtConfig.Value;
        }
        public string GenerateToken(IdentityUser identityUser, string role)
        {
            var secretKey = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);
            var issuer = _jwtConfig.Issuer;
            var audience = _jwtConfig.Audience;

            var tokenHandler = new JwtSecurityTokenHandler();
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Name, identityUser.NormalizedUserName),
                new Claim(ClaimTypes.Role, role)
            };

            

            var claimTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(claimTokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
