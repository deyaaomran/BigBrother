using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Mapping.Students
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student , StudentDto>().ReverseMap();
            CreateMap<Student , Asisstant>().ReverseMap();
        }
    }
}
