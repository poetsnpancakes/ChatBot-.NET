using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.LLM.Services.Interfaces;
using OpenAI.Chat;
using System.Text;
using ChatBot.Models.Request;


namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BotController : ControllerBase
    {
        //public IOpenAIService _openAIService;

        private readonly IBotService _botService;
        public BotController( IBotService botService)
        {
           
            _botService = botService;
        }


        [HttpGet("query")]
        public async Task<IActionResult> Prompt(String request)
        {
            var response= await _botService.QueryBot(request);
            var result = new
            {
                query = request,
                response = response
            };
            return Ok(result);
        }
        [HttpPost("ask")]
        public async Task<IActionResult> ChatStream([FromBody] PromptRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Prompt))
                {
                    Console.WriteLine("PromptRequest is null or empty");
                    return BadRequest("Prompt is required.");
                }

                Response.ContentType = "text/event-stream";
                Response.Headers.Add("Cache-Control", "no-cache");
                Response.Headers.Add("X-Accel-Buffering", "no");

                await foreach (var chunk in _botService.ResponsiveQueryBot(request.Prompt, request.SessionId))
                {
                    var data = $"data: {chunk}\n\n";
                    var buffer = Encoding.UTF8.GetBytes(data);
                    await Response.Body.WriteAsync(buffer, 0, buffer.Length);
                    await Response.Body.FlushAsync();
                }

                return new EmptyResult(); // ✅ You must return something
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Streaming error: " + ex.Message);
                Console.WriteLine("❌ Stack Trace:\n" + ex.StackTrace);
                return StatusCode(500, $"Streaming failed: {ex.Message}");
            }
        }

    }
}
