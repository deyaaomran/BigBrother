using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Student : BaseEntity
    {
        public int PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Attendance> attendances { get; set; }
        public ICollection<Course> courses { get; set; }
    }
}
