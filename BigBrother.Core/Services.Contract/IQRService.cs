using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IQRService
    {
        Task<IActionResult> DownloadStudentQRCodesAsync(int courseId);
    }
}
