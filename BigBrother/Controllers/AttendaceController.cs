using BigBrother.Core.Dtos;
using BigBrother.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.APIs.Controllers
{
    public class AttendaceController : BaseApiController
    {
        private readonly IAttendanceServices _attendanceServices;

        public AttendaceController(IAttendanceServices attendanceServices)
        {
            _attendanceServices = attendanceServices;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attend = await _attendanceServices.GetAllAttendanceAsync();
            return Ok(attend);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendaceById(int id)
        {
            var attend = await _attendanceServices.GeAttendanceByIdAsync(id);
            if (attend.Value == null)
            {
                return NotFound($"Attendance with ID {id} not found.");
            }
            return Ok(attend);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> ScanStudent(AttendanceDto attendance)
        {
            if (attendance == null)
                return BadRequest("Invalid student data.");

            bool isSuccess = await _attendanceServices.RegisterAttendanceAsync(attendance);

            if (!isSuccess)
                return StatusCode(500, $"Student {attendance.StudentName} is already registered for this lecture today.");

            return Ok(new { message = "Student attendance recorded successfully!", attendance });
        }
        [HttpGet("for-student/{studentId}")]
        public async Task<IActionResult> AttendaceForStudent(int studentId)
        {
            var Attendace = await _attendanceServices.GetAttentaceForStudentAsync(studentId);
            return Ok(Attendace);
        }
        [HttpGet("for-course/{courseId}")]
        public async Task<IActionResult> AttendaceForCourse(int courseId)
        {
            var Attendace = await _attendanceServices.GetAttentaceForCourseAsync(courseId);
            return Ok(Attendace);
        }
    }
}
