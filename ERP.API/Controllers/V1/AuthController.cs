using ERP.API.Models;
using ERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ERP.API.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginRequest request)
        //{

        //    return Ok("Test");
        //}

        //[HttpGet]
        //public async Task<ActionResult> Test()
        //{
        //    return Ok("Test");
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            var user =
                await _userRepository
                    .GetByUsernameAsync(request.Username);

            if (user is null)
            {
                return Unauthorized(
                    new { message = "Invalid username or password" });
            }

            bool validPassword =
                _passwordHasher.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!validPassword)
            {
                return Unauthorized(
                    new { message = "Invalid username or password" });
            }

            var token =
                _jwtTokenGenerator.GenerateToken(user);

            var response =
                new LoginResponse
                {
                    Token = token,
                    Username = user.Username,
                    Role = user.Role
                };

            return Ok(response);
        }
    }
    
    //[ApiController]
    //[Route("api/auth")]
    //public class AuthController : ControllerBase
    //{
    //    public AuthController()
    //    { }

    //    [HttpPost("login")]
    //    public async Task<IActionResult> Login(LoginRequest request)
    //    {

    //        return Ok("Test");
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult> Test()
    //    {
    //        return Ok("Test");
    //    }
    //}
}
