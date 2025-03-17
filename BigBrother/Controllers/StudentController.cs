using BigBrother.APIs.Attributes;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.APIs.Controllers
{
    
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadStudents(IFormFile file , int courseid)
        {
            if (file == null || file.Length == 0) return BadRequest("Please upload a valid file.");

            using var stream = file.OpenReadStream();
            await _studentService.UploadStudentsAsync(stream , courseid);

            return Ok("Students uploaded successfully.");
        }

        //[HttpGet("all")]
        //[Cached(30)]
        //public async Task<IActionResult> GetAllStudents()
        //{
        //    var students = await _studentService.GetStudentsAsync();
        //    return Ok(students);
        //}
        [HttpGet("{StdId}")]
        public async Task<IActionResult> GetStudentById(int StdId)
        {
            var student = await _studentService.GetStudentByIdAsync(StdId);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddStudent(StudentDto student , int courseid)
        {
            await _studentService.AddStudentAsync(student, courseid);
            return Ok("Student added Sucssefuly");

        }
        [HttpGet("by-course/{CourseId}")]
        public async Task<IActionResult> StudentForCourse(int CourseId)
        {
            var result = await _studentService.GetStudentsOfCourseAsync(CourseId);

            
            if (result.Result is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }
            else if (result.Result is NotFoundObjectResult notFound)
            {
                return NotFound(notFound.Value); 
            }
            else if (result.Result is ObjectResult statusCodeResult && statusCodeResult.StatusCode >= 400)
            {
                return StatusCode(statusCodeResult.StatusCode.Value, statusCodeResult.Value); 
            }

            
            return Ok(result.Result);
        }
    }
}
