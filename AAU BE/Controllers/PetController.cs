using System.Data;
using AAU_BE.Database;
using AAU_BE.Models;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace AAU_BE.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly PetService _petService; //instantiate the todo service


        public PetController(ILogger<PetController> logger)
        {
            _logger = logger;
            _petService = new PetService();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAllPetsForUser(int id)
        {
            var todos = _petService.GetPetsForUser(id);
            if (todos != null)
            {
                return Ok(todos);
            }
            return NotFound(new { Message = "No Pets found for the user." });
        }
        
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_petService.DeletePet(id))
            {
                return Ok();
            }
            return BadRequest(new { Message = "Please Try again later" });
        }

        [HttpPost]
        
        public IActionResult CreatePet([FromBody] Pet newPet)
        {  
            if (_petService.CreatePet(newPet))
            {
                return Ok(new { Message = "Pet created successfully" });
            }
            return BadRequest(new { Message = "Failed to create Pet" });
        }
        
        [HttpPatch("{id}")]
        public IActionResult UpdatePet([FromBody] Pet updatedPet)
        {
            if (_petService.UpdatePet(updatedPet))
            {
                return Ok(new { Message = "ToDo updated successfully" });
            }
            return BadRequest(new { Message = "Failed to update ToDo" });
        }
    }

}