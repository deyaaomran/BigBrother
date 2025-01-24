using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Instructor : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
