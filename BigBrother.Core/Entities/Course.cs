using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Student> students { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
