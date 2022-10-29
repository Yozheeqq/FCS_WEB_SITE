using FCS_WebSite.Services;
using FCS_WebSite_v2.Data.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using FCS_WebSite_v2.Data.Models;
using FCS_WebSite_v2.Data;
using FCS_WebSite.Models;

var builder = WebApplication.CreateBuilder(args);

// Root of DB configuration
IConfigurationRoot confRoot = new ConfigurationBuilder().SetBasePath(
    builder.Environment.ContentRootPath).AddJsonFile("Data/DB/dbsettings.json").Build();

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Add Sql Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(confRoot.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>().
        AddEntityFrameworkStores<ApplicationDbContext>().
        AddDefaultUI().AddDefaultTokenProviders();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registration}/{action=Index}/{id?}"); // ������� �� Home


var scope = app.Services.CreateScope();
DBObjects.Content = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await ContextSeed.SeedRolesAsync(roleManager);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred seeding the DB.");
}

app.Run();
