using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Services.Services
{
    public class InstructorServices : IInstructorServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InstructorServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCourseAsync(CourseDto course)
        {
            var cour = _mapper.Map<Course>(course);
            await _context.AddAsync(cour);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CourseDto>> GetCourseAsync()
        {
            var course = await _context.courses.ToListAsync();
            return _mapper.Map<List<CourseDto>>(course);
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

       

        
    }
}
