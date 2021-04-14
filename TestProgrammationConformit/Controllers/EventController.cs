using Microsoft.AspNetCore.Mvc;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/events")]
    public class EventController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new string[] {"event-001"});
        }
    }
}