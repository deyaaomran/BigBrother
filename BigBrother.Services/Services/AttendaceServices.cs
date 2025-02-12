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
        public async Task<AttendanceDto> GeAttendanceByIdAsync(int id)
        {
            var attend =await _context.attendances.Where(s => s.Id == id).FirstOrDefaultAsync();
             var attendace = _mapper.Map<AttendanceDto>(attend);
            attendace.StudentName = _context.students.Where(s => s.Id == id).FirstOrDefault().Name;
            return attendace;


        }

        public async Task<List<AttendanceDto>> GetAllAttendanceAsync()
        {
            var attend = await _context.attendances.ToListAsync();
            var attendace = _mapper.Map<List<AttendanceDto>>(attend);
            attendace.ForEach(a => { a.StudentName = _context.students.Where(s => s.Id == a.StudentId).FirstOrDefault().Name; });
            return attendace;
        }

        public async Task<bool> RegisterAttendanceAsync(AttendanceDto attend)
        {

            var studentAttendance = new AttendanceDto()
            {
                StudentId = attend.StudentId,
                StudentName = attend.StudentName,
                Date = DateTime.Now,
                Time= new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0),
                AsisstantId = 2,
                CourseId = 1,
                
            };

            var att = _mapper.Map<Attendance>(studentAttendance);
            await _context.AddAsync(att);
            int result = await _context.SaveChangesAsync();
            if (result == 0) return false;
            else return true;           
        }
    }
}
