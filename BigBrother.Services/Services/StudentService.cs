using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using ClosedXML.Excel;
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

        public StudentService( AppDbContext context)
        {
            _context = context;
        }

        public async Task UploadStudentsAsync(Stream excelFile)
        {
            using var workbook = new XLWorkbook(excelFile);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed();

            foreach (var row in rows.Skip(1))
            { // Skip header
                var student = new Student
                {
                    Name = row.Cell(1).GetValue<string>(),
                    Email = row.Cell(2).GetValue<string>(),
                    PhoneNumber = row.Cell(3).GetValue<int>()
                    //courseId = row.Cell(4).GetValue<int>()
                };

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
