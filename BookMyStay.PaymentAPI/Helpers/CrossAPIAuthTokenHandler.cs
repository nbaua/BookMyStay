
using Microsoft.AspNetCore.Authentication;

namespace BookMyStay.PaymentAPI.Helpers
{
    public class CrossAPIAuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CrossAPIAuthTokenHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            if (token != null)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
