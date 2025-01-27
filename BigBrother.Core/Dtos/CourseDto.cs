using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartFrom { get; set; }
        public TimeSpan EndIn { get; set; }
        public DayOfWeek DayOfCourse { get; set; }
        public int InstructorId { get; set; }
    }
}
