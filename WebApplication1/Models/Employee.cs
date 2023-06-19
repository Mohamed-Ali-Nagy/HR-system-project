using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
      
        public int Phone { get; set; }
       

        [ForeignKey("department")]
        public int DeptID { get; set; }

       
        public  Department ?department { get; set; }
        public List<Attendance>? attendances { get; set; } = new List<Attendance>();
    }
}
