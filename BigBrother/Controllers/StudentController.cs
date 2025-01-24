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

        [HttpGet("absentees")]
        public IActionResult GetAbsentees(int courseId)
        {
            var absentees = _studentService.GetAbsentees(courseId);
            return Ok(absentees);
        }
    }
}
