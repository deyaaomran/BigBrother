using BigBrother.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.Core.Services.Contract
{
    public interface IAttendanceServices
    {
        Task<List<AttendanceDto>> GetAllAttendanceAsync();
        Task<ActionResult<AttendanceDto>> GeAttendanceByIdAsync(int id);
        Task <bool> RegisterAttendanceAsync(AttendanceDto attend);
        Task<ActionResult<List<AttendanceDto>>> GetAttentaceForCourseAsync(int CourseId);
        Task<List<StudentCourseDto>> GetCountOfCourseAttendanceAsync(int CourseId);
        Task<ActionResult<List<AttendanceDto>>> GetAttentaceForStudentAsync(int StudentId);


    }
}
