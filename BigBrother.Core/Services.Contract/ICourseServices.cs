﻿using BigBrother.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface ICourseServices
    {
        Task AddCourseAsync(CourseDto course);
        Task<List<CourseDto>> GetCourseAsync(int instructorid);

    }
}
