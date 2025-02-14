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
        Task AddAsisstantAsync(int StudentId , int CourseId);
        Task ApproveAsisstantAsync(int asisstantId);
        Task SendCode(int asisstantId , string code);
        
        //Task SetAttendanceFile(Stream excelFile);
    }
}
