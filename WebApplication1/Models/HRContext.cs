using Microsoft.EntityFrameworkCore;

namespace HRSystem.Models
{
    public class HRContext:DbContext
    {

        public HRContext(DbContextOptions options):base(options) { }
       
       public DbSet<Employee> Employees { get; set; }
       // public DbSet<Attendance> Attendances { get; set; }
        //public DbSet<Holidays> Holidays { get; set; }

       // public DbSet<GeneralSettings> GeneralSettings { get; set; }
        


    }
}
