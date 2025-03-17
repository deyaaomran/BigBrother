using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace BigBrother.APIs.Controllers
{
    public class AsisstantController : BaseApiController
    {
        private readonly IAsisstantService _asisstantService;

        public AsisstantController(IAsisstantService asisstantService)
        {
            _asisstantService = asisstantService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsisstant(AsisstantAccountDto asisstant)
        {
            await _asisstantService.AddAsisstantAsync(asisstant);
            return Ok("Asisstant added successfully.");
        }
        [HttpGet("asisstants-for-course{courseId}")]
        public async Task<IActionResult> GetAsisstantsForCourse(int courseId)
        {
            var asisstants = await _asisstantService.GetAsisstantsForCourseAsync(courseId);
            return Ok(asisstants);
        }

    }
}
