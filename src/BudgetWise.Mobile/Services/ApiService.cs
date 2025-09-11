using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace BudgetWise.Mobile.Services;

public class ApiService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public ApiService(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private async Task SetAuthHeader()
    {
        var token = await _auth.GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<T>?> GetListAsync<T>(string endpoint)
    {
        await SetAuthHeader();
        var response = await _http.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<T>>();
        }
        Console.WriteLine($"Error fetching list from {endpoint}: {response.StatusCode}");
        return null;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        await SetAuthHeader();
        var response = await _http.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        Console.WriteLine($"Error fetching item from {endpoint}: {response.StatusCode}");
        return default;
    }

    public async Task<bool> PostAsync<T>(string url, T data)
    {
        try
        {
            await SetAuthHeader();
            var response = await _http.PostAsJsonAsync(url, data);
            if (response.IsSuccessStatusCode) return true;

            Console.WriteLine($"Error in POST to {url}: {response.StatusCode}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in POST: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> PutAsync<T>(string endpoint, T data)
    {
        try
        {
            await SetAuthHeader();
            var response = await _http.PutAsJsonAsync(endpoint, data);
            Console.WriteLine($"PUT {endpoint} - Status: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ PUT request failed: {error}");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in PUT: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        try
        {
            await SetAuthHeader();
            var response = await _http.DeleteAsync(endpoint);
            if (response.IsSuccessStatusCode) return true;

            Console.WriteLine($"Error in DELETE from {endpoint}: {response.StatusCode}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DELETE: {ex.Message}");
            return false;
        }
    }
}
