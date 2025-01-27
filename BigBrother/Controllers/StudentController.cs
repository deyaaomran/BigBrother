using BigBrother.Core.Services.Contract;
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
        public async Task<IActionResult> UploadStudents(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Please upload a valid file.");

            using var stream = file.OpenReadStream();
            await _studentService.UploadStudentsAsync(stream);

            return Ok("Students uploaded successfully.");
        }

        [HttpGet("getallstudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetStudentsAsync();
            return Ok(students);
        }
        [HttpGet("getstudentbyid")]
        public async Task<IActionResult> GetStudentById(int StdId)
        {
            var student = await _studentService.GetStudentAsync(StdId);
            return Ok(student);
        }
    }
}
