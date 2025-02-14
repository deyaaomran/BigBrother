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

        [HttpGet("getallattendace")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attend = await _attendanceServices.GetAllAttendanceAsync();
            return Ok(attend);
        }
        [HttpGet("getattendacebyid")]
        public async Task<IActionResult> GetAttendaceById(int id)
        {
            var attend = await _attendanceServices.GeAttendanceByIdAsync(id);
            return Ok(attend);
        }
        [HttpPost("uploadAttendance")]
        public async Task<IActionResult> ScanStudent( AttendanceDto attendance)
        {
            if (attendance == null)
                return BadRequest("Invalid student data.");

            bool isSuccess = await _attendanceServices.RegisterAttendanceAsync(attendance);

            if (!isSuccess)
                return StatusCode(500, "Error saving student attendance.");

            return Ok(new { message = "Student attendance recorded successfully!", attendance });
        }
        [HttpGet("forstudent")]
        public async Task<IActionResult> AttendaceForStudent(int studentId)
        {
            var Attendace = await _attendanceServices.GetAttentaceForStudentAsync(studentId);
            return Ok(Attendace);
        }
        [HttpGet("forcourse")]
        public async Task<IActionResult> AttendaceForCourse(int courseId)
        {
            var Attendace = await _attendanceServices.GetAttentaceForCourseAsync(courseId);
            return Ok(Attendace);
        }
    }
}
