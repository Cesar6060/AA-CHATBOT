using System.Data;
using AAU_BE.Database;
using AAU_BE.Models;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AAU_BE.Controllers

{
    [ApiController]
    [Route("user")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginService _loginService; //instantiate the login service


        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _loginService = new LoginService();
        }

        // POST: /login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _loginService.AuthenticateUser(loginRequest.username, loginRequest.password);

            if (user != null)
            {
                return Ok(new
                {
                    Message = "Login successful",
                    UserId = user.UserId,
                    UserLevel = user.UserLevel
                });
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }
        //POST /Register
        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequest loginRequest)
        {
            if (_loginService.AddUser(loginRequest))
            {
                return Ok(new { Message = "User Add successful" });
            }
            return Unauthorized(new { Message = "Please Try again later" });
        }
    }
}