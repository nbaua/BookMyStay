using AutoMapper;
using BookMyStay.DBLoggerAPI.Data;
using BookMyStay.DBLoggerAPI.Services;
using BookMyStay.MessageBroker;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddSingleton<IMessageHandler, MessageHandler>();
builder.Services.AddScoped<IDBLogger, DBLogger>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
