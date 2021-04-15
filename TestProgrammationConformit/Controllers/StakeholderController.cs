using Microsoft.AspNetCore.Mvc;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/stakeholders")]
    public class StakeholderController : BaseController<Stakeholder, int>
    {
        public StakeholderController(IService<Stakeholder, int> service) : base(service, 0)
        {
        }
    }
}
