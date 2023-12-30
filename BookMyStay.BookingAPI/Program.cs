using AutoMapper;
using BookMyStay.BookingAPI.Data;
using BookMyStay.BookingAPI.Helpers;
using BookMyStay.BookingAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region DBConnection
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});
#endregion

#region AutoMapper

IMapper mapper = DataMapperConfig.RegisterMappings().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion


//register the http client side handler to adding tokens on API
builder.Services.AddHttpContextAccessor();//Need this for fetching the token from client
builder.Services.AddScoped<CrossAPIAuthTokenHandler>();//assign the same token to cross API

//for inter service communication
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddHttpClient("Listing",
    c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ListingAPI"])).AddHttpMessageHandler<CrossAPIAuthTokenHandler>();

builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddHttpClient("Offer",
    c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:OfferAPI"])).AddHttpMessageHandler<CrossAPIAuthTokenHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Swagger API changes
//add authentication to swagger documentation
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT bearer token - `Bearer -{TOKEN}`",
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme//"Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme} //"Bearer" 
        }, new List<string>() }
    });
});

#endregion

#region Application Authentication Using JWT

var SecretKey = builder.Configuration.GetValue<string>("ApiConfigurations:SecretKey");
var Issuer = builder.Configuration.GetValue<string>("ApiConfigurations:Issuer");
var Audience = builder.Configuration.GetValue<string>("ApiConfigurations:Audience");

var key = Encoding.ASCII.GetBytes(SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //"Bearer" 
}
).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience
    };
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
