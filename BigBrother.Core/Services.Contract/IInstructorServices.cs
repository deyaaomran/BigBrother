using BigBrother.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IInstructorServices
    {
        Task AddCourseAsync(CourseDto course);
        Task<List<CourseDto>> GetCourseAsync();
        Task UploadStudentsAsync(Stream excelFile);
        Task<List<StudentDto>> GetStudentsAsync();
        Task<StudentDto> GetStudentAsync(int Studentid);

        



    }
}
