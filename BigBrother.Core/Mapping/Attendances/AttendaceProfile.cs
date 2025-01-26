using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Mapping.Attendances
{
    public class AttendaceProfile: Profile
    {
        public AttendaceProfile()
        {
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
        }
    }
}
