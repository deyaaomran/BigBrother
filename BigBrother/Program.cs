
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BigBrother.Core.Entities.Identity;
using BigBrother.Core.Mapping.Attendances;
using BigBrother.Core.Mapping.Courses;
using BigBrother.Core.Mapping.Students;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using BigBrother.Repository.Identity.Context;
using BigBrother.Services.Chaches;
using BigBrother.Services.QRService;
using BigBrother.Services.Services;
using BigBrother.Services.Token;
using BigBrother.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.Membership;
using BigBrother.Core.Mapping.Asisstant;


namespace BigBrother.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Environment.SetEnvironmentVariable("EPPlusLicenseContext", "NonCommercial");
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero 
                    };
                });
            builder.Services.AddAuthorization();


            // Add services to the container.
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("OfflineConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false,connectTimeout=10000,syncTimeout=10000"));


         


            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IAsisstantService, AsisstantService>();
            builder.Services.AddScoped<ICourseServices, CourseServices>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IQRService, QRService>();
            //builder.Services.AddScoped<IServiceProvider, ServiceProvider>();
            builder.Services.AddScoped<IInstructorServices, InstructorServices>();
            builder.Services.AddScoped<IAttendanceServices, AttendaceServices>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new AttendaceProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new StudentProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new CourseProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new AsisstantProfile()));
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
