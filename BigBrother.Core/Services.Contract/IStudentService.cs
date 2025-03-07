using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IStudentService
    {
        Task UploadStudentsAsync(Stream excelFile , int CourseId);
        Task AddStudentAsync(StudentDto student , int courseid);
        //Task<List<StudentDto>>  GetStudentsAsync(int courseid);
        Task<StudentDto> GetStudentByIdAsync (int Studentid);
        Task<ActionResult<List<StudentDto>>> GetStudentsOfCourseAsync(int CourseId);
        Task<List<StudentDto>> GetStudentsOfCourseQRAsync(int courseId);
    }
}
