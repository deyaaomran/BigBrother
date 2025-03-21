﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Entities
{
    public class Attendance : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; } 
        public bool IsPresent { get; set; } = true;
        /*--------------------------------*/


        public int? StudentId { get; set; }
        public int? CourseId { get; set; }

        public StudentCourses studentCourses { get; set; }


        /*--------------------------------------*/
        public Asisstant asisstant { get; set; }
        public int AsisstantId { get; set; }
    }
}
