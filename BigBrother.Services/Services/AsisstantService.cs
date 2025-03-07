using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using System.IO;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BigBrother.Services.Services
{
    public class AsisstantService : IAsisstantService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
       

        public AsisstantService(AppDbContext context , IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task AddAsisstantAsync(int StudentId , int CourseId)
        {
            var Asisst =await _context.students.Where(s => s.Id == StudentId).FirstOrDefaultAsync();
            if ( Asisst == null ) throw new Exception("Student not found.");
            var Checkasisst =await _context.asisstants.Where(s => s.Id == Asisst.Id).FirstOrDefaultAsync();
            if (Checkasisst is not null) throw new Exception("Asisstant Allready Founded");
            else 
            {
                var Asisstant = _mapper.Map<Asisstant>(Asisst);
                await GenerateAsisstantCodeAsync(Asisstant.Id);
                Asisstant.CourseId = CourseId;
                _context.asisstants.Add(Asisstant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveAsisstantAsync(int asisstantId)
        {
            var assistant = await _context.asisstants.FindAsync(asisstantId);
            if (assistant == null) throw new Exception("Assistant not found.");

            assistant.Status = true;
            await _context.SaveChangesAsync();
        }

        private async Task GenerateAsisstantCodeAsync(int asisstantId)
        {
            var code = HelperService.GenerateCode();
            var asisst = await _context.asisstants.Where(a => a.Id == asisstantId).FirstOrDefaultAsync();
            if (asisst != null)
            { 
                asisst.Code = code; 
                await _context.SaveChangesAsync();
            }
        }

        public Task SendCode(int asisstantId, string code)
        {
            var fromAddress = new MailAddress("your_email@example.com", "BigBrother");
            var asisstant = _context.asisstants.Where(a => a.Id ==  asisstantId).FirstOrDefault();
            var toAddress = new MailAddress(asisstant.Email);
            const string fromPassword = "your_email_password";
            const string subject = "Your Registration Code";
            string body = $"Your registration code is: {code}";

            var smtp = new SmtpClient
            {
                Host = "smtp.example.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return Task.CompletedTask;
        }

        //public async Task SetAttendanceFile(Stream excelFile)
        //{



        //    // استخدم مكتبة EPPlus لمعالجة الملف

        //    var package = new ExcelPackage(excelFile);
        //    var worksheet = package.Workbook.Worksheets[0];
        //    int rowCount = worksheet.Dimension.Rows;

        //    // اقرأ البيانات من الصفوف
        //    for (int row = 2; row <= rowCount; row++) // افترض أن الصف الأول هو العناوين
        //    {
        //        var attend = new AttendanceDto()
        //        {
        //            StudentId = worksheet.Cells[row, 1].GetValue<int>(),
        //            StudentName = worksheet.Cells[row, 2].GetValue<string>(),
        //            CourseId = worksheet.Cells[row, 3].GetValue<int>(),
        //            Date = worksheet.Cells[row, 4].GetValue<DateTime>().Date,
        //            Time =  worksheet.Cells[row,5].GetValue<TimeSpan>(),
        //            AsisstantId = worksheet.Cells[row, 6].GetValue<int>()
        //        };
        //        attend.Time = new TimeSpan(0, 0, 0);
        //        var attendance = _mapper.Map<Attendance>(attend);
        //        _context.attendances.Add(attendance);

        //    }
        //    await _context.SaveChangesAsync();





        //}
       

    }
}
