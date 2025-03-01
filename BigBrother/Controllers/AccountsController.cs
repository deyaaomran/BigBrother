using BigBrother.Core.Dtos;
using BigBrother.Core.Entities.Identity;
using BigBrother.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BigBrother.APIs.Controllers
{
    
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountsController(
            IUserService userService, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")] // post : /api/Accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           var user = await _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized();
            return Ok(user);
        }
        [HttpPost("register")] // post : /api/Accounts/login
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);
            if (user is null) return BadRequest("Invalid Registeration !!");
            return Ok(user);
        }
        [HttpGet("GerCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return BadRequest();
            var user = await _userManager.FindByEmailAsync(userEmail);
            if(user is null) return BadRequest();   
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
    }
}
