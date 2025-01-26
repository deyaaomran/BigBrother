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
        public async Task AddAsisstantAsync(Asisstant asisstant)
        {
            asisstant.Status = "Pending";
            _context.asisstants.Add(asisstant);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveAsisstantAsync(int asisstantId)
        {
            var assistant = await _context.asisstants.FindAsync(asisstantId);
            if (assistant == null) throw new Exception("Assistant not found.");

            assistant.Status = "Approved";
            await _context.SaveChangesAsync();
        }

        public async Task SetAttendanceFile(Stream excelFile)
        {

            

            // استخدم مكتبة EPPlus لمعالجة الملف

            var package = new ExcelPackage(excelFile);
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            // اقرأ البيانات من الصفوف
            for (int row = 2; row <= rowCount; row++) // افترض أن الصف الأول هو العناوين
            {
                var attend = new AttendanceDto()
                {
                    StudentId = worksheet.Cells[row, 1].GetValue<int>(),
                    StudentName = worksheet.Cells[row, 2].GetValue<string>(),
                    CourseId = worksheet.Cells[row, 3].GetValue<int>(),
                    Date = worksheet.Cells[row, 4].GetValue<DateTime>().Date,
                    Time =  worksheet.Cells[row,5].GetValue<TimeSpan>(),
                    AsisstantId = worksheet.Cells[row, 6].GetValue<int>()
                };
                attend.Time = new TimeSpan(0, 0, 0);
                var attendance = _mapper.Map<Attendance>(attend);
                _context.attendances.Add(attendance);
               
            }
            await _context.SaveChangesAsync();





        }
    }
}
