using BigBrother.Core.Dtos;
using BigBrother.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.APIs.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices )
        {
            _courseServices = courseServices;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddCourse(CourseDto course)
        {
            if (course == null) return BadRequest();
            await _courseServices.AddCourseAsync(course);
            return Ok();
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseServices.GetCourseAsync();
            return Ok(courses);
        }
    }
}
