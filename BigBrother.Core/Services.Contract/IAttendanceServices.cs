using BigBrother.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IAttendanceServices
    {
        Task<List<AttendanceDto>> GetAllAttendanceAsync();
        Task<AttendanceDto> GeAttendanceByIdAsync(int id);
        Task <bool> RegisterAttendanceAsync(AttendanceDto attend);


    }
}
