using Microsoft.EntityFrameworkCore;
using HRSystem.ViewModels;

namespace HRSystem.Models
{
    public class HRContext:DbContext
    {

        public HRContext() : base()
        {

        }
        public HRContext(DbContextOptions options):base(options) 
        {
        
        
        }
       
       public  virtual DbSet<Employee> Employees { get; set; }
         public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Department>Departments { get; set; }
       

        //public DbSet<Holidays> Holidays { get; set; }

        // public DbSet<GeneralSettings> GeneralSettings { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HRDB;Integrated Security=True;TrustServerCertificate=True");
        //}


    }
}
