using System.Data;
using AAU_BE.Database;
using AAU_BE.Models;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace AAU_BE.Controllers
{
    [ApiController]
    [Route("appointments")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly ToDoService _toDoService; //instantiate the todo service


        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
            _toDoService = new ToDoService();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAllTodos(int id)
        {
            var todos = _toDoService.GetAllTodos(id);
            if (todos != null)
            {
                return Ok(todos);
            }
            return NotFound(new { Message = "No ToDos found for the user." });
        }
        
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_toDoService.DeleteToDo(id))
            {
                return Ok();
            }
            return BadRequest(new { Message = "Please Try again later" });
        }

        [HttpPost]
        
        public IActionResult CreateTodo([FromBody] Appointment newAppointment)
        {  
            if (_toDoService.CreateTodo(newAppointment))
            {
                return Ok(new { Message = "ToDo created successfully" });
            }
            return BadRequest(new { Message = "Failed to create ToDo" });
        }
        
        [HttpPatch("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] Appointment updatedTodAppointment)
        {
            if (_toDoService.UpdateTodo(id, updatedTodAppointment))
            {
                return Ok(new { Message = "ToDo updated successfully" });
            }
            return BadRequest(new { Message = "Failed to update ToDo" });
        }
    }

}