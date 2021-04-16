using Microsoft.AspNetCore.Mvc;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/events")]
    public class EventController : BaseController<Event, int>
    {
        public EventController(IService<Event, int> service, int identity) : base(service, identity)
        {
        }
    }
}
