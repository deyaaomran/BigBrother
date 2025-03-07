using BigBrother.Core.Entities;
using BigBrother.Repository.Data.Context;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using BigBrother.Core.Services.Contract;
using AutoMapper;
using BigBrother.Services.Services;

namespace BigBrother.Services.QRService
{
    public class QRService: IQRService
    {
        private readonly AppDbContext _context;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public QRService(AppDbContext context, IStudentService studentService, IMapper mapper)
        {
            _context = context;
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> DownloadStudentQRCodesAsync(int courseId)
        {
            var studentsDtoResult = await _studentService.GetStudentsOfCourseQRAsync(courseId);
            if (studentsDtoResult is null)
            {
                return new NotFoundObjectResult("No students found for the given course.");
            }
            
           

            var students = _mapper.Map<List<Student>>(studentsDtoResult);

            string tempFolder = Path.Combine(Path.GetTempPath(), "QRCodeFiles");

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);

            string zipFilePath = Path.Combine(tempFolder, "Students_QRCodes.zip");

            using (var archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (var student in students)
                {
                    string qrText = $"{student.Id},{student.Name},{student.Department}";
                    byte[] qrCodeBytes = GenerateQRCode(qrText);

                    var entry = archive.CreateEntry($"{student.Name}_QR.png", CompressionLevel.Optimal);
                    using (var entryStream = entry.Open())
                    {
                        await entryStream.WriteAsync(qrCodeBytes, 0, qrCodeBytes.Length);
                    }
                }
            }

            var memoryStream = new MemoryStream(await File.ReadAllBytesAsync(zipFilePath));
            return new FileStreamResult(memoryStream, "application/zip") { FileDownloadName = "Students_QRCodes.zip" };
        }

        private static byte[] GenerateQRCode(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                {
                    BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                    return qrCode.GetGraphic(20);
                }
            }
        }
    }
}
