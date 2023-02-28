using E_Commerce.WEB.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IdentityModelEventSource.ShowPII = true;
builder.Services.AddControllersWithViews();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddOIDC();
var app = builder.Build();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    RequireHeaderSymmetry = false,
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.Run();