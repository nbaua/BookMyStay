using BookMyStay.WebApp.Helpers;

namespace BookMyStay.WebApp.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public void SetToken( string tokenValue)
        {
            string Key = Constants.TokenKeyPrefix;
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(Key, tokenValue);
        }

        public string? GetToken()
        {
            string Key = Constants.TokenKeyPrefix;
            string? token = null;
            bool isToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(Key, out token);
            return isToken ? token : null;
        }

        public void ClearToken()
        {
            string Key = Constants.TokenKeyPrefix;
            string? token = null;
            bool isToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(Key, out token);
            if (isToken)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(Key);
            }
        }

    }
}
