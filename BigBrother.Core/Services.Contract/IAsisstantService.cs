using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IAsisstantService
    {
        Task<UserDto> AddAsisstantAsync(AsisstantAccountDto asisstant);
        Task<UserDto> LoginAsisstantAsync(AsisstantLoginDto loginDto);
        Task<List<Asisstant>> GetAsisstantsForCourseAsync(int CourseId);
        //Task ApproveAsisstantAsync(int asisstantId);
        //Task SendCode(int asisstantId , string code);

        //Task SetAttendanceFile(Stream excelFile);
    }
}
