using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            ConformitContext = conformitContext;
        }

        private ConformitContext ConformitContext { get; }

        [HttpGet]
        public IEnumerable<Stakeholder> Index() => ConformitContext.Stakeholders.ToList();

        // TODO: Handle System.Linq.ThrowHelper.ThrowNoElementsException() => HTTP 404
        [HttpGet("{id:int}")]
        public Stakeholder Details(int id) => ConformitContext.Stakeholders.FirstOrDefault(_ => _.Id == id);
    }
}