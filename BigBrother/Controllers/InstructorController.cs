using BigBrother.Core.Dtos;
using BigBrother.Core.Services.Contract;
using BigBrother.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.APIs.Controllers
{
    public class InstructorController : BaseApiController
    {
        private readonly IInstructorServices _instructorServices;

        public InstructorController(IInstructorServices instructorServices)
        {
            _instructorServices = instructorServices;
        }
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(CourseDto course)
        {
            await _instructorServices.AddCourseAsync(course);
            return Ok("Course added Sucssefuly");

        }
        



    }
}
