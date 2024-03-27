using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Authontication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TicTacToeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper Mapper;

        private IAuthService authService;

        public AuthenticationController(IAuthService authService, IMapper Mapper)
        {
            this.Mapper = Mapper;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserSignupDTO userSignupDTO, string role = "User") 
        {
            var registerResult = await this.authService.RegisterAsync(userSignupDTO, role);

            if (registerResult.IsSucceed)
            {
                return Ok(registerResult);
            }

            return BadRequest(registerResult);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var loginResult = await this.authService.LoginAsync(userLoginDTO);

            if (loginResult.IsSucceed)
            {
                return Ok(loginResult);
            }

            return BadRequest(loginResult);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var logoutResult = await this.authService.LogoutAsync();

            if (logoutResult.IsSucceed)
            {
                return Ok(logoutResult);
            }

            return BadRequest(logoutResult);
        }

        [HttpGet]
        public async Task<IActionResult> Me()
        {
            var userNameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
            var userId = Guid.Parse(userNameIdentifier);

            var meResult = await this.authService.MeAsync(userId);

            if (meResult.IsSucceed)
            {
                return Ok(meResult);
            }

            return BadRequest(meResult);
        }
    }
}
