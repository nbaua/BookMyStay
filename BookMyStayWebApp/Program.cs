using BookMyStay.MessageBroker;
using BookMyStay.WebApp.Helpers;
using BookMyStay.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;



var builder = WebApplication.CreateBuilder(args);

Constants.AuthApiEndPoint = builder.Configuration["APIEndPoints:AuthAPI"];
Constants.OfferApiEndPoint = builder.Configuration["APIEndPoints:OfferAPI"];
Constants.ListingApiEndPoint = builder.Configuration["APIEndPoints:ListingAPI"];
Constants.BookingApiEndPoint = builder.Configuration["APIEndPoints:BookingAPI"];


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IMessageHandler, MessageHandler>(); //RabbitMQ Message Broker Service.

//register end-points with http client
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IOfferService, OfferService>();
builder.Services.AddHttpClient<IListingService, ListingService>();
builder.Services.AddHttpClient<IBookingService, BookingService>();

//register the service base as scoped dependencies
builder.Services.AddScoped<IServiceBase, ServiceBase>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    }
    );

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
