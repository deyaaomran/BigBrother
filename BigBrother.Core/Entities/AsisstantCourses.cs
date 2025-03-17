using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class AsisstantCourses
    {
        public int AsisstantId { get; set; }
        public Asisstant Asisstant { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        

    }
}
