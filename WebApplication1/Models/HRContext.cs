using Microsoft.EntityFrameworkCore;

namespace HRSystem.Models
{
    public class HRContext:DbContext
    {

        public HRContext(DbContextOptions options):base(options) 
        {
            
        }
        
       public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        //public DbSet<Holidays> Holidays { get; set; }

        public DbSet<GeneralSettings> GeneralSettings { get; set; }



    }
}
