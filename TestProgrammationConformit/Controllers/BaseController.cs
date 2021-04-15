using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformit.Controllers
{
    public abstract class BaseController<TModel, TId> : Controller where TModel : Model<TId>
    {
        protected BaseController(IService<TModel, TId> service, TId identity)
        {
            Identity = identity;
            Service = service;
        }

        protected IService<TModel, TId> Service { get; }
        protected TId Identity { get; }


        [HttpGet]
        public ActionResult<IEnumerable<TModel>> Index() => Ok(Service.Find(2, 5));

        [HttpDelete("{id}")]
        public ActionResult<TModel> Delete([FromRoute] TId id) => Service.Delete(id) ? NotFound() : NoContent();

        [HttpPost]
        [HttpPut("{id}")]
        public ActionResult<TModel> Persist([FromRoute] TId id, [FromBody] TModel stakeholder)
        {
            if (!Identity.Equals(id))
            {
                stakeholder.Id = id;
            }

            stakeholder = Service.Persist(stakeholder);

            return null == stakeholder ? NotFound() : Ok(stakeholder);
        }

        [HttpGet("{id}")]
        public ActionResult<TModel> Details([FromRoute] TId id)
        {
            var stakeholder = Service.Find(id);
            return null == stakeholder ? NotFound() : Ok(stakeholder);
        }
    }
}
