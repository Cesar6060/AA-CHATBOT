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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService; //instantiate the login service


        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = new UserService();
        }
        
        [HttpPatch("changepassword")]
        public IActionResult ChangePassword([FromBody] User user)
        {
            if (_userService.UpdateUserPassword(user.id, user.password)) 
            {
                return Ok(new { Message = "Password Change successful" });
            }
            return BadRequest(new { Message = "Please Try again later" });
        }
        
        [HttpPatch("updaterole")]
        public IActionResult UpdateRole([FromBody] User user)
        {
            if (_userService.UpdateUser(user)) 
            {
                return Ok(new { Message = "User update successful" });
            }
            return BadRequest(new { Message = "Please Try again later" });
        }

        [HttpGet("all")]
        public IActionResult AllUsers()
        {
            List<User> users = _userService.GetAllUsers();
            return Ok(users);
        }
        
        [HttpGet("name/{id}")]
        public IActionResult UserName(int id)
        {
            string username = _userService.GetUserName(id);
            return Ok(username);
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            if (_userService.DeleteUser(id))
            {
                return Ok();
            }
            return BadRequest(new { Message = "Please Try again later" });
        }
    }
}