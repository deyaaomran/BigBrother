using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Asisstant : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Course course { get; set; }
        public int CourseId { get; set; }
        public ICollection<Attendance> attendances { get; set; }
    }
}
