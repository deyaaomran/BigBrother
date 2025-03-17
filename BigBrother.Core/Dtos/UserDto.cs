using BigBrother.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string DisplayName   { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public List<AsissCourseDto> courses { get; set; }
        public int asisstantId { get; set; }
    }
}
