using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Order_Taker.client.API.Extentions;
using Order_Taker.client.API.Helpers;
using Order_Taker.Core.Reposatories;
using Order_Taker.Repositoriy.Data;
using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy.Reposatories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Databases(builder);
builder.Services.ApplicationService();

var app = builder.Build();
using var Scope = app.Services.CreateScope();
var Services = Scope.ServiceProvider;
await MigrationService.Migrations(Services, app);


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
