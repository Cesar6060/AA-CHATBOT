using System.Data;
using AAU_BE.Database;
using AAU_BE.Models;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AAU_BE.Controllers
{
    [ApiController]
    [Route("faq")]
    public class FaqController : ControllerBase
    {
        private readonly ILogger<FaqController> _logger;
        private readonly FaqService _faqService; //instantiate the login service


        public FaqController(ILogger<FaqController> logger)
        {
            _logger = logger;
            _faqService = new FaqService();
        }
        
         [HttpPatch("update")]
         public IActionResult Login([FromBody] FAQ faq)
         {
             if (_faqService.UpdateFaQ(faq)) 
             {
                 return Ok(new { Message = "FAQ Change successful" });
             }
             return BadRequest(new { Message = "Please Try again later" });
         }
        
        [HttpPost("add")]
        public IActionResult Insert([FromBody] FAQ faq)
        {
            if (_faqService.InsertFaq(faq))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public IActionResult ALlFaq()
        {
            List<FAQ> faqs = _faqService.GetAllFAQS();
            return Ok(faqs);
        }
        
        
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            if (_faqService.DeleteFaq(id))
            {
                return Ok();
            }
            return BadRequest(new { Message = "Please Try again later" });
        }
    }
    
}