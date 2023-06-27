using Microsoft.EntityFrameworkCore;
using HRSystem.ViewModels;

namespace HRSystem.Models
{
    public class HRContext:DbContext
    {

        public HRContext(DbContextOptions options):base(options) 
        {
            
        }
        public HRContext() : base() { }
       
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Holidays> Holidays { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }

        //public DbSet<HRSystem.ViewModels.EmployeeDepartmentVM> EmployeeDepartmentVM { get; set; } = default!;
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HRsystem;Integrated Security=True;TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
