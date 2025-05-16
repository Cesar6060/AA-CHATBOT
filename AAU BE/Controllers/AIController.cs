using AAU_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AAU_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIController : ControllerBase
    {
        private readonly ILogger<AIController> _logger;
        private readonly string _apiKey;

        public AIController(ILogger<AIController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrEmpty(_apiKey))
            {
                _logger.LogError("OpenAI API key is not configured.");
                throw new InvalidOperationException("OpenAI API key is not configured.");
            }
        }
        
        [HttpPost("generatebio")]
        public async Task<IActionResult> GeneratePetBio([FromBody] ChatRequest chatRequest)
        {
            Console.WriteLine("this is my prompt" + chatRequest?.Prompt);
            if (chatRequest?.Prompt == null)
            {
                _logger.LogWarning("Prompt is null or missing");
                return BadRequest("Prompt cannot be null");
            }

            // Get response from OpenAI API
            string response = await GetGptResponse("Hi Can you generate me a Pet Bio with these descriptions: " + chatRequest.Prompt);

            // Log the response for debugging
            _logger.LogInformation("Received response from OpenAI: {response}", response);

            return Ok(response);
        } 

        // Message Action
        [HttpPost]
        public async Task<IActionResult> Message([FromBody] ChatRequest chatRequest)
        {
            if (chatRequest?.Prompt == null)
            {
                _logger.LogWarning("Prompt is null or missing");
                return BadRequest("Prompt cannot be null");
            }

            // Get response from OpenAI API
            string response = await GetGptResponse(chatRequest.Prompt);

            // Log the response for debugging
            _logger.LogInformation("Received response from OpenAI: {response}", response);

            return Ok(response);
        }

        //GetGPTResponse Method
        private async Task<string> GetGptResponse(string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return "Error: Prompt is null or empty";
            }

            var client = new RestClient("https://api.openai.com");

            var restRequest = new RestRequest("/v1/chat/completions", Method.Post);

            restRequest.AddHeader("Authorization", $"Bearer {_apiKey}");
            restRequest.AddHeader("Content-Type", "application/json");

            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            restRequest.AddJsonBody(body);

            // Execute the request asynchronously
            var response = await client.ExecuteAsync(restRequest);

            if (response == null)
            {
                _logger.LogError("OpenAI API response is null");
                return "Error: OpenAI API response is null";
            }

            if (!response.IsSuccessful)
            {
                _logger.LogError("OpenAI API returned an error: {StatusCode} {Content}", response.StatusCode, response.Content);
                return $"Error: OpenAI API returned an error - {response.StatusDescription}";
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                _logger.LogError("OpenAI API response content is empty");
                return "Error: OpenAI API response content is empty";
            }

            try
            {
                var jsonResponse = JObject.Parse(response.Content);
                var content = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString().Trim();

                if (string.IsNullOrEmpty(content))
                {
                    _logger.LogError("No valid content in OpenAI API response");
                    return "Error: No valid content in OpenAI API response";
                }

                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing response from OpenAI API");
                return $"Error: Unable to parse OpenAI API response - {ex.Message}";
            }
        }
    }

    // ChatRequest class to handle incoming requests
    public class ChatRequest
    {
        public string? Prompt { get; set; }
    }
}
