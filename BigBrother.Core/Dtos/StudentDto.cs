using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        //public long PhoneNumber { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        //public string Email { get; set; }
        public string? Department { get; set; }
    }
}
