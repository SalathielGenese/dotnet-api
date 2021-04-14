using Microsoft.AspNetCore.Mvc;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/comments")]
    public class CommentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new string[] {"comment-001-jfk"});
        }
    }
}