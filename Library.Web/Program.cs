using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Library.Web.Data;
using Library.Web.Services.Abstractions;
using Library.Web.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//----------------------------------------------------------------------------------------------

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

builder.Services.AddScoped<IAuthorsService, AuthorsService>();
//builder.Services.AddTransient<IAuthorsService, AuthorsService>();
//builder.Services.AddSingleton<IAuthorsService, AuthorsService>();

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5; 
    config.IsDismissable = true; 
    config.Position = NotyfPosition.BottomRight;
});
//----------------------------------------------------------------------------------------------
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
//----------------------------------------------------------------------------------------------

app.UseNotyf();

//----------------------------------------------------------------------------------------------

app.Run();
