
using AutoMapper;
using BigBrother.Core.Mapping.Attendances;
using BigBrother.Core.Mapping.Courses;
using BigBrother.Core.Mapping.Students;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using BigBrother.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BigBrother.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("EPPlusLicenseContext", "NonCommercial");
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IAsisstantService, AsisstantService>();
            builder.Services.AddScoped<ICourseServices, CourseServices>();
            builder.Services.AddScoped<IAttendanceServices, AttendaceServices>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new AttendaceProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new StudentProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new CourseProfile()));
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Attendance System API", Version = "v1" });

               
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()  // ?????? ????? ??? Domains
                              .AllowAnyMethod()  // ?????? ????? ??? Methods (GET, POST, PUT, DELETE, ...)
                              .AllowAnyHeader(); // ?????? ????? ??? Headers
                    });
            });


            var app = builder.Build();
            app.UseCors("AllowAll");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
