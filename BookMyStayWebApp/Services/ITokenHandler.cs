namespace BookMyStay.WebApp.Services
{
    public interface ITokenHandler
    {
        string? GetToken();
        void SetToken(string tokenValue);
        void ClearToken();
    }
}
