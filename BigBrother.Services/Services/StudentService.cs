using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using QRCoder;
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

        public async Task UploadStudentsAsync(Stream excelFile, int courseId)
        {
            try
            {
                var package = new ExcelPackage(excelFile);
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                var studentsToAdd = new List<Student>();
                var students = new List<Student>();
                var studentCoursesToAdd = new List<StudentCourses>();

                for (int row = 4; row <= rowCount; row++) // افترض أن الصف الأول هو العناوين
                {
                    var code = worksheet.Cells[row, 2].GetValue<int?>();
                    var name = worksheet.Cells[row, 3].GetValue<string>();
                    var department = worksheet.Cells[row, 4].GetValue<string>();

                    if (string.IsNullOrEmpty(name))
                    {
                        // تخطي الصف لو الـName فارغ
                        continue;
                    }

                    var student = new Student
                    {
                        Code = code,
                        Name = name,
                        Department = department
                        
                    };

                    if (code.HasValue)
                    {
                        var existingStudent = await _context.students.Where(s => s.Code == code).FirstOrDefaultAsync();
                        if (existingStudent != null)
                        {
                            
                            existingStudent.Name = name;
                            existingStudent.Department = department;
                          
                            _context.students.Update(existingStudent);
                            students.Add(student);
                        }
                        else
                        {
                            studentsToAdd.Add(student);
                            students.Add(student);
                        }
                    }
                    else
                    {
                        studentsToAdd.Add(student);
                        students.Add(student);
                    }
       
                }
                
                _context.students.AddRange(studentsToAdd);

                await _context.SaveChangesAsync();


                foreach(var std in students)
                {
                    var studentCourse = new StudentCourses
                    {
                        StudentId =  _context.students.Where(s => s.Code == std.Code).FirstOrDefault().Id,
                        CourseId = courseId
                    };
                    studentCoursesToAdd.Add(studentCourse);

                }
                _context.studentCourses.AddRange(studentCoursesToAdd);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("An error occurred while uploading students.", ex);
            }
        }

        //public async Task<List<StudentDto>> GetStudentsAsync(int courseid)
        //{
        //    var stdcourse = await _context.studentCourses.Where(sc => sc.CourseId == courseid).ToListAsync();
        //    var std = await _context.students.ToListAsync();
        //    var students = _mapper.Map<List<StudentDto>>(std);
        //    if (students == null || !students.Any())
        //    {
        //        return new List<StudentDto>(); // Return an empty list instead of NotFoundResult
        //    }
        //    return students;
        //}

        public async Task<StudentDto> GetStudentByIdAsync(int Studentid)
        {
            var std = await _context.students.Where(s => s.Id == Studentid).FirstOrDefaultAsync();

            if (std == null)
            {
                
                return null;
            }
            var student = _mapper.Map<StudentDto>(std);

            return student;
        }
        public async Task AddStudentAsync(StudentDto student, int courseid)
        {
            if (student.Code != null)
            {
                var exsist = await _context.students.FindAsync(student.Code);

                if (exsist != null)
                {
                    // 🔄 الطالب موجود، يمكنك تحديث بياناته أو تخطيه
                    exsist.Name = student.Name;
                    _context.students.Update(exsist);
                }
                else
                {

                    _context.students.Add(_mapper.Map<Student>(student));
                }

            }
            else
            {
                await _context.AddAsync(_mapper.Map<Student>(student));
            }
            await _context.SaveChangesAsync();
            
            var studentCourse = new StudentCourses
            {
                StudentId =  _context.students.Where(s => s.Code == student.Code).FirstOrDefault().Id,
                CourseId = courseid
            };
            await _context.studentCourses.AddAsync(studentCourse);

            await _context.SaveChangesAsync();
        }
       

        public async Task<ActionResult<List<StudentDto>>> GetStudentsOfCourseAsync(int courseId)
        {
            try
            {
                // تحقق من أن الـcourseId صحيح
                if (courseId <= 0)
                {
                    return new BadRequestObjectResult("Invalid Course ID.");
                }

                // جلب الطلاب المسجلين في الكورس
                var studentsInCourse = await _context.studentCourses
                    .Where(s => s.CourseId == courseId)
                    .Join(
                        _context.students,
                        sc => sc.StudentId,
                        st => st.Id,
                        (sc, st) => st
                    )
                    .ToListAsync();

                // تحقق من وجود الطلاب
                if (studentsInCourse == null || !studentsInCourse.Any())
                {
                    return new NotFoundObjectResult("No students found for the given course.");
                }

                // تحويل البيانات إلى DTO
                var studentsDto = _mapper.Map<List<StudentDto>>(studentsInCourse);

                // إرجاع النتيجة
                return new OkObjectResult(studentsDto);
            }
            catch (Exception ex)
            {
                

                
                return new StatusCodeResult(500);
            }
        }
        public async  Task<List<StudentDto>> GetStudentsOfCourseQRAsync(int courseId)
        {
            try
            {
                // تحقق من أن الـcourseId صحيح
                if (courseId <= 0)
                {
                    return null; // Return an empty list instead of BadRequest
                }

                // جلب الطلاب المسجلين في الكورس
                var studentsInCourse = await _context.studentCourses
                    .Where(s => s.CourseId == courseId)
                    .Join(
                        _context.students,
                        sc => sc.StudentId,
                        st => st.Id,
                        (sc, st) => st
                    )
                    .ToListAsync();

                // تحقق من وجود الطلاب
                if (studentsInCourse == null || !studentsInCourse.Any())
                {
                    return null;
                }

                // تحويل البيانات إلى DTO
                var studentsDto = _mapper.Map<List<StudentDto>>(studentsInCourse);

                // إرجاع النتيجة
                return studentsDto;
            }
            catch (Exception ex)
            {
                return new List<StudentDto>(); // Return an empty list instead of StatusCodeResult
            }
        }
    }
}
