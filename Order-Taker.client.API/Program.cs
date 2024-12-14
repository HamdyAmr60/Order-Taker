using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Order_Taker.client.API.Helpers;
using Order_Taker.Core.Reposatories;
using Order_Taker.Repositoriy.Data;
using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy.Reposatories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderTakerDBContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<AppIdentityDbContext>(Options => 
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnetion"));
});
builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
{
    var connection = builder.Configuration.GetConnectionString("redis");
    return ConnectionMultiplexer.Connect(connection);
});
builder.Services.AddScoped(typeof(IOrderTakerRepo<>) , typeof(OrderTakerRepo<>));
//builder.Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
builder.Services.AddScoped<IBasketRepo,BasketRepo>();
builder.Services.AddAutoMapper(M => M.AddProfile(typeof(Profiles)));
builder.Services.AddScoped<ProductPhotoResolver>();
var app = builder.Build();
using var Scope = app.Services.CreateScope();
var Services = Scope.ServiceProvider;
var _dbContext = Services.GetRequiredService<OrderTakerDBContext>() ;
var _Identity = Services.GetRequiredService<AppIdentityDbContext>();

var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

try
{

    await _dbContext.Database.MigrateAsync();
    await _Identity.Database.MigrateAsync();
     await OrderTakerSeed.DataSeed(_dbContext);
}
catch (Exception ex)
{
    var Logger = LoggerFactory.CreateLogger<Program>();
    Logger.LogError(ex, "an error occured while migration");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();




app.Run();
