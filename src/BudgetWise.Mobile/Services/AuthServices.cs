using Microsoft.Maui.Storage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BudgetWise.Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly JwtAuthenticationStateProvider _authProvider;

        public AuthService(JwtAuthenticationStateProvider authProvider)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5240/")
            };
            _authProvider = authProvider;
        }

        private async Task SetAuthHeader()
        {
            var token = await GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<string?> Register(string email, string password)
        {
            var data = new { Email = email, Password = password };
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("api/User/register", content);
            var body = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"ℹ️ Register response: {response.StatusCode} - {body}");
            return response.IsSuccessStatusCode ? null : body;
        }

        public async Task<(bool success, string? error)> Login(string email, string password)
        {
            var data = new { Email = email, Password = password };
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("api/User/login", content);
            var body = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"ℹ️ Login response: {response.StatusCode} - {body}");
            if (!response.IsSuccessStatusCode)
                return (false, body);

            var doc = JsonDocument.Parse(body);
            var token = doc.RootElement.GetProperty("token").GetString();

            if (!string.IsNullOrWhiteSpace(token))
            {
                await SecureStorage.SetAsync("auth_token", token);
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                _authProvider.NotifyUserAuthentication(token);
                return (true, null);
            }
            return (false, "Token is missing.");
        }

        public async Task Logout()
        {
            await SecureStorage.SetAsync("auth_token", string.Empty);
            _authProvider.NotifyUserLogout();
        }

        public async Task<string?> GetToken()
            => await SecureStorage.GetAsync("auth_token");

        public async Task<int?> GetUserIdFromToken()
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token)) return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
                return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing token: {ex.Message}");
                return null;
            }
        }
    }
}
