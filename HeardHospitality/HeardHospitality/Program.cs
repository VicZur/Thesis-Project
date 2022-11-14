using HeardHospitality.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();



//namespace HeardHospitality
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var host = CreateHostBuilder(args).Build();

//            CreateDbIfNotExists(host);

//            host.Run();

//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(connectionString));
//            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddEntityFrameworkStores<ApplicationDbContext>();
//            builder.Services.AddControllersWithViews();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseMigrationsEndPoint();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");
//            app.MapRazorPages();

//            app.Run();


//            //host.Run();
//        }
//        private static void CreateDbIfNotExists(IHost host)
//        {
//            using (var scope = host.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                try
//                {
//                    var context = services.GetRequiredService<ApplicationDbContext>();
//                    DbInitializer.Initialize(context);
//                }
//                catch (Exception ex)
//                {
//                    var logger = services.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred creating the DB.");
//                }
//            }
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}


