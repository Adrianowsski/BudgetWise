using Microsoft.Extensions.Logging;
using BudgetWise.Mobile.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace BudgetWise.Mobile;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>() // ← nie zmieniamy na Blastro!
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // ✅ HttpClient z poprawnym adresem API backendu
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5240/")
        });

        // ✅ Autoryzacja i serwisy
        builder.Services.AddScoped<JwtAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<ApiService>(); // ← ważne!
        builder.Services.AddAuthorizationCore();

        return builder.Build();
    }
}

