using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.Controllers
{
    [ApiController]
    [Route("/stakeholders")]
    public class StakeholderController : Controller
    {
        public StakeholderController(ConformitContext conformitContext)
        {
            StakeholdersDbSet = conformitContext.Stakeholders;
            ConformitContext = conformitContext;
        }

        private DbSet<Stakeholder> StakeholdersDbSet { get; }
        private ConformitContext ConformitContext { get; }

        [HttpGet]
        public ActionResult<IEnumerable<Stakeholder>> Index() => Ok(StakeholdersDbSet.ToList());

        [HttpGet("{id:int}")]
        public ActionResult<Stakeholder> Details([FromRoute] int id)
        {
            var firstOrDefault = StakeholdersDbSet.FirstOrDefault(_ => _.Id == id);

            return null == firstOrDefault ? NotFound() : Ok(firstOrDefault);
        }

        [HttpPost]
        public ActionResult<Stakeholder> Create([FromBody] Stakeholder stakeholder)
        {
            stakeholder = StakeholdersDbSet.Add(stakeholder).Entity;
            ConformitContext.SaveChanges();

            return CreatedAtAction(nameof(Details), new {id = stakeholder.Id}, stakeholder);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Stakeholder> Update(
            [FromBody] Stakeholder stakeholder,
            [FromRoute] int id)
        {
            var firstOrDefault = StakeholdersDbSet.FirstOrDefault(_ => _.Id == id);

            if (null == firstOrDefault)
            {
                return NotFound();
            }

            firstOrDefault.Name = stakeholder.Name;
            stakeholder = StakeholdersDbSet.Update(firstOrDefault).Entity;
            ConformitContext.SaveChanges();

            return Ok(stakeholder);
        }
    }
}