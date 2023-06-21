using HRSystem.Models;
using HRSystem.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HRContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            );
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IEmployeeRepositry,EmployeeRepository>();
            builder.Services.AddScoped<IGeneralSettingRepository,GeneralSettingRepository>();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IHolidaysRepository, HolidaysRepository>();
            builder.Services.AddScoped<HRContext, HRContext>();


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
        }
    }
}