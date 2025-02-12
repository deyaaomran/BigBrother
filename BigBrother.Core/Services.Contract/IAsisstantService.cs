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
        Task AddAsisstantAsync(int StudentId);
        Task ApproveAsisstantAsync(int asisstantId);
        Task SetAttendanceFile(Stream excelFile);
    }
}
