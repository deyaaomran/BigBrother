using AutoMapper;
using BigBrother.Core.Dtos;
using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using System.IO;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using BigBrother.Core.Entities.Identity;
using BigBrother.Services.Users;
using Microsoft.AspNetCore.Identity;
using BigBrother.Services.Token;

namespace BigBrother.Services.Services
{
    public class AsisstantService : IAsisstantService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AsisstantService(AppDbContext context , IMapper mapper , UserManager<AppUser> userManager, ITokenService tokenService , SignInManager<AppUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        public async Task<UserDto> AddAsisstantAsync(AsisstantAccountDto asisstant)
        {
            var asisst = _mapper.Map<Asisstant>(asisstant);
            var exsist = await _context.asisstants.Where(a => a.Name == asisstant.Name).FirstOrDefaultAsync();
            if (exsist == null)
            {
                if (!await CheckNameExistAsync(asisst.Name)) _context.asisstants.Add(asisst); ;
                await _context.SaveChangesAsync();

                
            }
            else
            {
                var asisstantcourse = new AsisstantCourses()
                {
                    AsisstantId = _context.asisstants.Where(a => a.Name == asisstant.Name).FirstOrDefault().Id,
                    CourseId = asisstant.CourseId
                };
                await _context.asisstantCourses.AddAsync(asisstantcourse);

                await _context.SaveChangesAsync();
            }
            


            var user = new AppUser()
            {
                PhoneNumber = asisstant.PhoneNumber,
                DisplayName = asisstant.Name,
                UserName = asisstant.Name,
                

            };
            var result = await _userManager.CreateAsync(user, asisstant.Password);
            if (!result.Succeeded) return null;

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

        }
        public async Task<bool> CheckNameExistAsync(string name)
        {
            return await _userManager.FindByNameAsync(name) is not null;
        }

        public async Task<List<Asisstant>> GetAsisstantsForCourseAsync(int CourseId)
        {
            var asisstants = await _context.asisstantCourses.Where(s => s.CourseId == CourseId).Select(s=>s.AsisstantId).ToListAsync();
            if (!asisstants.Any()) return null;
            var asisstlist = await _context.asisstants.Where(c => asisstants.Contains(c.Id)).Select(a => new Asisstant { Name = a.Name, Id = a.Id }).ToListAsync();
            return asisstlist;

        }

        public async Task<UserDto> LoginAsisstantAsync(AsisstantLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null) return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return null;
            var asisstId =  _context.asisstants.Where(a => a.Name == loginDto.UserName).FirstOrDefault().Id;
            var couRses = await _context.asisstantCourses.Where(a => a.AsisstantId == asisstId).Select(a =>a.CourseId).ToListAsync();
            var courselist = await _context.courses.Where(c => couRses.Contains(c.Id)).Select(a => new AsissCourseDto {CourseId = a.Id,CourseName = a.Name }).ToListAsync();

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                asisstantId = asisstId,
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                courses = courselist
                

            };
        }



        #region Edited
        //public async Task ApproveAsisstantAsync(int asisstantId)
        //{
        //    var assistant = await _context.asisstants.FindAsync(asisstantId);
        //    if (assistant == null) throw new Exception("Assistant not found.");

        //    assistant.Status = true;
        //    await _context.SaveChangesAsync();
        //}

        //private async Task GenerateAsisstantCodeAsync(int asisstantId)
        //{
        //    var code = HelperService.GenerateCode();
        //    var asisst = await _context.asisstants.Where(a => a.Id == asisstantId).FirstOrDefaultAsync();
        //    if (asisst != null)
        //    { 
        //        asisst.Code = code; 
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public Task SendCode(int asisstantId, string code)
        //{
        //    var fromAddress = new MailAddress("your_email@example.com", "BigBrother");
        //    var asisstant = _context.asisstants.Where(a => a.Id ==  asisstantId).FirstOrDefault();
        //    var toAddress = new MailAddress(asisstant.Email);
        //    const string fromPassword = "your_email_password";
        //    const string subject = "Your Registration Code";
        //    string body = $"Your registration code is: {code}";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.example.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //    };
        //    using (var message = new MailMessage(fromAddress, toAddress)
        //    {
        //        Subject = subject,
        //        Body = body
        //    })
        //    {
        //        smtp.Send(message);
        //    }
        //    return Task.CompletedTask;
        //}

        //public async Task SetAttendanceFile(Stream excelFile)
        //{



        //    // استخدم مكتبة EPPlus لمعالجة الملف

        //    var package = new ExcelPackage(excelFile);
        //    var worksheet = package.Workbook.Worksheets[0];
        //    int rowCount = worksheet.Dimension.Rows;

        //    // اقرأ البيانات من الصفوف
        //    for (int row = 2; row <= rowCount; row++) // افترض أن الصف الأول هو العناوين
        //    {
        //        var attend = new AttendanceDto()
        //        {
        //            StudentId = worksheet.Cells[row, 1].GetValue<int>(),
        //            StudentName = worksheet.Cells[row, 2].GetValue<string>(),
        //            CourseId = worksheet.Cells[row, 3].GetValue<int>(),
        //            Date = worksheet.Cells[row, 4].GetValue<DateTime>().Date,
        //            Time =  worksheet.Cells[row,5].GetValue<TimeSpan>(),
        //            AsisstantId = worksheet.Cells[row, 6].GetValue<int>()
        //        };
        //        attend.Time = new TimeSpan(0, 0, 0);
        //        var attendance = _mapper.Map<Attendance>(attend);
        //        _context.attendances.Add(attendance);

        //    }
        //    await _context.SaveChangesAsync();





        //} 
        #endregion


    }
}
