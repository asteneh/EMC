using CNET_ERP_V7.Common.AuthNavigation;
using CNET_ERP_V7.Common.Company;
using CNET_ERP_V7.Common.Helpers;
using CNET_ERP_V7.WebConstants;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
var configuration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .Build();
var baseUrl = configuration.GetValue<string>("CnetApiBaseUrl");
builder.Services.AddHttpClient("mainclient", client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<AuthenticationManager>();
builder.Services.AddScoped<NavigatorManager>();
builder.Services.AddScoped<SharedHelpers>();
builder.Services.AddSingleton<MapSstore>();
builder.Services.AddAuthentication(CNET_WebConstantes.CookieScheme) // Sets the default scheme to cookies
     .AddCookie(CNET_WebConstantes.CookieScheme, options =>
     {
         options.AccessDeniedPath = "/account/denied";
         options.LoginPath = "/login";
     });
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine("C:\\inetpub\\wwwroot\\SharedFiles", "node_modules")),
    RequestPath = "/node_modules"
});
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
