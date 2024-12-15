using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPing()
        {
            return Ok(new { Message = "API is working!" });
        }
    }
}
