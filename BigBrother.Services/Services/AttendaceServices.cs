using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Services.Services
{
    
    public class AttendaceServices : IAttendanceServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AttendaceServices(AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<List<AttendanceDto>> GetAllAttendanceAsync()
        {
            var attend = await _context.attendances.ToListAsync();
            var attendace = _mapper.Map<List<AttendanceDto>>(attend);
            attendace.ForEach(a => { a.StudentName = _context.students.Where(s => s.Id == a.StudentId).FirstOrDefault().Name; });
            return attendace;
        }

        public async Task<ActionResult<List<AttendanceDto>>> GetAttentaceForCourseAsync(int CourseId)
        {
            try
            {
                if (CourseId <= 0)
                {
                    throw new ArgumentException("Invalid Course ID.");
                }
                var att = await _context.attendances.Where(s => s.CourseId == CourseId).ToListAsync();
                var attendace = _mapper.Map<List<AttendanceDto>>(att);
                return attendace;
            }
            catch (Exception ex)
            {

                return new ObjectResult("An error occurred while processing your request.")
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<ActionResult<List<AttendanceDto>>> GetAttentaceForStudentAsync(int StudentId)
        {
            if (StudentId <= 0)
            {
                throw new ArgumentException("Invalid Course ID.");
            }
            try
            {
                var att = await _context.attendances.Where(s => s.StudentId == StudentId).ToListAsync();
                var attendace = _mapper.Map<List<AttendanceDto>>(att);
                return attendace;
            }
            catch (Exception)
            {

                return new ObjectResult("An error occurred while processing your request.")
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<List<StudentCourseDto>> GetCountOfCourseAttendanceAsync(int courseId)
        {
            
            if (courseId <= 0)
            {
                throw new ArgumentException("Invalid Course ID.");
            }

            var studentAttendanceCounts = await (
                from sc in _context.studentCourses
                where sc.CourseId == courseId
                join a in _context.attendances
                on new { sc.StudentId, sc.CourseId } equals new { StudentId = a.StudentId.Value, CourseId = a.CourseId.Value } into attendanceGroup
                select new StudentCourseDto
                {
                    StudentName = _context.students.Where(s => s.Id == sc.StudentId).FirstOrDefault().Name,
                    CourseName = _context.courses.Where(s => s.Id == sc.CourseId).FirstOrDefault().Name,
                    CountofAttend = attendanceGroup.Count() 
                }
            ).ToListAsync();

            return studentAttendanceCounts;
        }

        public async Task<bool> RegisterAttendanceAsync(AttendanceDto attend)
        {
            
            var existingAttendance = await _context.attendances
                .FirstOrDefaultAsync(a => a.StudentId == attend.StudentId
                                          && a.CourseId == attend.CourseId
                                          && a.Date.Date == DateTime.Now.Date);

            if (existingAttendance != null)
            {
                
                return false;
            }


            var studentAttendance = new AttendanceDto()
            {
                StudentId = attend.StudentId,
                StudentName = attend.StudentName,
                Date = DateTime.Now,
                Time= new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0),
                AsisstantId = attend.AsisstantId,
                CourseId = attend.CourseId                
            };

            var att = _mapper.Map<Attendance>(studentAttendance);
            await _context.AddAsync(att);
            int result = await _context.SaveChangesAsync();
            return result > 0;           
        }

        public async Task<ActionResult<AttendanceDto>> GeAttendanceByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult("Invalid ID. ID must be greater than 0.");
            }

            try
            {

                var attend = await _context.attendances
                    .Where(s => s.Id == id)
                    .FirstOrDefaultAsync();


                if (attend == null)
                {
                    return new NotFoundObjectResult($"Attendance with ID {id} not found.");
                }


                var attendanceDto = _mapper.Map<AttendanceDto>(attend);


                var student = await _context.students
                    .Where(s => s.Id == attendanceDto.StudentId)
                    .FirstOrDefaultAsync();

                if (student != null)
                {
                    attendanceDto.StudentName = student.Name;
                }
                else
                {
                    attendanceDto.StudentName = "Unknown";
                }

                return new OkObjectResult(attendanceDto);
            }
            catch (Exception ex)
            {

                return new ObjectResult("An error occurred while processing your request.")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
