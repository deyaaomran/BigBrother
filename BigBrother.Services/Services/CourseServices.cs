using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Services.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CourseServices(AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCourseAsync(CourseDto course)
        {
           var cour = _mapper.Map<Course>(course);
            await _context.AddAsync(cour);
            await _context.SaveChangesAsync();  
        }
        
    }
}
