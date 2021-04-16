using Microsoft.AspNetCore.Mvc;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/comments")]
    public class CommentController : BaseController<Comment, int>
    {
        public CommentController(IService<Comment, int> service, int identity) : base(service, identity)
        {
        }
    }
}
