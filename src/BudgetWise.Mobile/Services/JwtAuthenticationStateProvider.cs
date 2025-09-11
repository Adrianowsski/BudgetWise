using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.Storage;
using System.Security.Claims;
using System.Text.Json;

namespace BudgetWise.Mobile.Services
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(_anonymous);

            try
            {
                var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public void NotifyUserAuthentication(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)!;

            foreach (var kvp in keyValuePairs)
            {
                switch (kvp.Key)
                {
                    case "sub":
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, kvp.Value.ToString()!));
                        break;
                    case "email":
                        claims.Add(new Claim(ClaimTypes.Email, kvp.Value.ToString()!));
                        claims.Add(new Claim(ClaimTypes.Name, kvp.Value.ToString()!));
                        break;
                    case "name":
                        claims.Add(new Claim(ClaimTypes.Name, kvp.Value.ToString()!));
                        break;
                    case "userId": // Dodano obsługę ID użytkownika
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, kvp.Value.ToString()!));
                        break;
                    default:
                        claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
                        break;
                }
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
