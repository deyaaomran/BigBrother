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
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAttendance(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Please upload a valid file.");

            // التحقق من نوع الملف (Excel)
            if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
                file.ContentType != "application/vnd.ms-excel")
            {
                return BadRequest("Invalid file type. Please upload an Excel file.");
            }
            using var stream = file.OpenReadStream();
            await _asisstantService.SetAttendanceFile(stream);

            return Ok("Attendance uploaded successfully.");
        }

    }
}
