using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using ClosedXML.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService( AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task UploadStudentsAsync(Stream excelFile)
        {
            // استخدم مكتبة EPPlus لمعالجة الملف

            var package = new ExcelPackage(excelFile);
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            // اقرأ البيانات من الصفوف
            for (int row = 2; row <= rowCount; row++) // افترض أن الصف الأول هو العناوين
            {
                var std = new StudentDto()
                {
                    Name = worksheet.Cells[row, 1].GetValue<string>(),
                    Email = worksheet.Cells[row, 2].GetValue<string>(),
                    PhoneNumber = worksheet.Cells[row, 3].GetValue<long>(),
                };
                var student = _mapper.Map<Student>(std);
                _context.students.Add(student);

            }
            await _context.SaveChangesAsync();
        }

       public ICollection<Student> GetAbsentees(int courseId)
        {
            
                
             var std = _context.studentCourses
            .Where(s => s.CourseId == courseId && !_context.attendances.Any(a => a.StudentId == s.StudentId))
            .Select(s => s.StudentId);

             return  _context.students
                             .Where(s => std.Contains(s.Id))
                             .ToList();
        }

       
    }
}
