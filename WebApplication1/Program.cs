using HRSystem.Models;
using HRSystem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Principal;

namespace WebApplication1
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HRContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            );
            builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 5000
            });

           

            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IEmployeeRepositry,EmployeeRepository>();
            builder.Services.AddScoped<IGeneralSettingRepository,GeneralSettingRepository>();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IHolidaysRepository, HolidaysRepository>();
            builder.Services.AddScoped<HRContext, HRContext>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<HRContext>().AddDefaultTokenProviders(); ;
               
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerfactory.CreateLogger("app");
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                HRSystem.Seeds.DefaultRole.seedRoleAsync(roleManager);
                HRSystem.Seeds.DefaultUser.seedUser(userManager);

                logger.LogInformation("data Seeded");
                logger.LogInformation("Application started");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An Error Occurred while seeding data");
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
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
        }
    }
}