using NLog;
using ShopBridge.DataAccess.Models;
using ShopBridge.Extensions;
using ShopBridge.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterDataAccess();
builder.Services.RegisterService();
builder.Services.RegisterValidationFilters();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

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

app.ConfigureExceptionHandler();
 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
