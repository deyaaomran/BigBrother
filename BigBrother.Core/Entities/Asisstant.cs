using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Asisstant : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        //public bool Status { get; set; } = false;
        //public string Code { get; set; }
        public ICollection<Course> courses { get; set; }
        public ICollection<Attendance> attendances { get; set; }
        
    }
}
