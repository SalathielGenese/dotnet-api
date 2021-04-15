using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/stakeholders")]
    public class StakeholderController : Controller
    {
        public StakeholderController(StakeholderService service)
        {
            Service = service;
        }

        private StakeholderService Service { get; }

        [HttpGet]
        public ActionResult<IEnumerable<Stakeholder>> Index() => Ok(Service.Find(2, 5));

        [HttpDelete("{id:int}")]
        public ActionResult<Stakeholder> Delete([FromRoute] int id) => Service.Delete(id) ? NotFound() : NoContent();

        [HttpPost]
        [HttpPut("{id:int}")]
        public ActionResult<Stakeholder> Persist([FromRoute] int id, [FromBody] Stakeholder stakeholder)
        {
            if (0 != id)
            {
                stakeholder.Id = id;
            }

            stakeholder = Service.Persist(stakeholder);
            return null == stakeholder ? NotFound() : Ok(stakeholder);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Stakeholder> Details([FromRoute] int id)
        {
            var stakeholder = Service.Find(id);
            return null == stakeholder ? NotFound() : Ok(stakeholder);
        }
    }
}
