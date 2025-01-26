using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Dtos
{
    public class AttendanceDto
    {
       
        public DateTime Date { get; set; }
        
        public TimeSpan Time { get; set; } = new TimeSpan (0,0,0);

        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int AsisstantId { get; set; }
    }
}
