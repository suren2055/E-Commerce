using System.Text;
using E_Commerce.WEB.ViewModels;
using E_Commerce.WEB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.WEB.Extensions;

public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("Settings"));
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderingService, OrderingService>();
        services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();
    }

    public static void AddOIDC(this IServiceCollection services)
    {
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "ecommerce";
                options.LoginPath = "/account/login";
                // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                // options.Cookie.HttpOnly = true;
                // options.Cookie.SameSite = SameSiteMode.Strict;
                // options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                // options.SlidingExpiration = true;
            })
            .AddOpenIdConnect(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = "http://host.docker.internal:8080/realms/ecommerce";
                options.ClientId = "ecommerce";
                options.ClientSecret = "X0aoSD2UkRso30HsJAx3SN3wBCUjL9z7";
                options.CallbackPath = "/account/login";
                options.SignedOutCallbackPath = "/signout-callback-oidc";
                options.RemoteSignOutPath = "/signout-oidc";
                options.Scope.Add(OpenIdConnectScope.OpenId);
                options.Scope.Add(OpenIdConnectScope.OpenIdProfile);
                options.ResponseType = OpenIdConnectResponseType.IdToken;
                options.ResponseMode = OpenIdConnectResponseMode.FormPost;

                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = "http://host.docker.internal:8080/realms/ecommerce",
                    ValidateAudience = false,
                    ValidAudience = "ecommerce",
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes("X0aoSD2UkRso30HsJAx3SN3wBCUjL9z7"))
                };
            });
    }
}