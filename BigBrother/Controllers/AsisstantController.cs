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
        [HttpPost("AddAsisstant")]
        public async Task<IActionResult> AddAsisstant(int studentId , int CourseId)
        {
             await _asisstantService.AddAsisstantAsync(studentId, CourseId);
            return Ok("Asisstant added successfully.");            
        }

    }
}
