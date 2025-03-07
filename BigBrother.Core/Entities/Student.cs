using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Student 
    {
        
        public int Id { get; set; }
        public int? Code { get; set; }
        //public long? PhoneNumber { get; set; }
        public string Name { get; set; }
        //public string Email { get; set; }
        public string? Department { get; set; }
        
        public ICollection<Attendance> attendances { get; set; }
        public ICollection<Course> courses { get; set; }
    }
}
