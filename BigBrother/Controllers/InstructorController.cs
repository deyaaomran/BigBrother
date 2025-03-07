using BigBrother.Core.Dtos;
using BigBrother.Core.Services.Contract;
using BigBrother.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.APIs.Controllers
{
    public class InstructorController : BaseApiController
    {
        private readonly IInstructorServices _instructorServices;
        private readonly IQRService _qRService;

        public InstructorController(IInstructorServices instructorServices
            ,IQRService qRService)
        {
            _instructorServices = instructorServices;
            _qRService = qRService;
        }
        [HttpPost("add-course")]
        public async Task<IActionResult> AddCourse(CourseDto course)
        {
            await _instructorServices.AddCourseAsync(course);
            return Ok("Course added Sucssefuly");

        }

        [HttpGet("download/{courseId}")]
        public async Task<IActionResult> DownloadStudentQRCodes(int courseId)
        {
          var qr= await _qRService.DownloadStudentQRCodesAsync(courseId);
            if (qr is NotFoundObjectResult notFound)
            {
                return NotFound(notFound.Value);
            }
            return qr;
        }

    }
}
