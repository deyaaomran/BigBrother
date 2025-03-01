using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
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
                    Id = worksheet.Cells[row, 1].GetValue<int>(),
                    Name = worksheet.Cells[row, 2].GetValue<string>(),
                    PhoneNumber = worksheet.Cells[row, 3].GetValue<long>(),
                    Email = worksheet.Cells[row, 4].GetValue<string>()
                    
                };
                var student = _mapper.Map<Student>(std);
                var exsist =  await _context.students.FindAsync(student.Id);
                if (exsist != null)
                {
                    // 🔄 الطالب موجود، يمكنك تحديث بياناته أو تخطيه
                    exsist.Name = student.Name;
                    _context.students.Update(exsist);
                }
                else
                {
                    _context.students.Add(student);
                }
                await _context.SaveChangesAsync();
            }
            
        }


        public async Task<List<StudentDto>> GetStudentsAsync()
        {
            var std = await _context.students.ToListAsync();
            var students = _mapper.Map<List<StudentDto>>(std);
            return students;
        }

        public async Task<StudentDto> GetStudentAsync(int Studentid)
        {
            var std = await _context.students.Where(s => s.Id == Studentid).FirstAsync();
            var student = _mapper.Map<StudentDto>(std);
            return student;
        }
        public async Task AddStudentAsync(StudentDto student)
        {
            var std = _mapper.Map<Student>(student);
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }
        public async Task<List<StudentDto>> GetStudentsOfCourseAsync(int CourseId)
        {
            var std = await _context.studentCourses.Where(s => s.CourseId == CourseId)
                .Join(
                        _context.students,
                        s => s.StudentId,
                        st => st.Id,
                        (s, st) => st
                     ).ToListAsync();
            var students = _mapper.Map<List<StudentDto>>(std);
            return students;

        }
    }
}
