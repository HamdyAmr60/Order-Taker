using Microsoft.AspNetCore.Identity;
using Order_Taker.client.API.Extentions;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Core;
using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Databases(builder);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
