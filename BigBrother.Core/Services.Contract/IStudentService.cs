using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IStudentService
    {
        Task UploadStudentsAsync(Stream excelFile);

        Task<List<StudentDto>>  GetStudentsAsync();
        Task<StudentDto> GetStudentAsync (int Studentid);
    }
}
