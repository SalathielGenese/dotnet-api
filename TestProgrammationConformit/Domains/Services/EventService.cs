using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.Domains.Services
{
    public class EventService: BaseService<Event, int>
    {
        public EventService(
            ConformitContext conformitContext,
            DbSet<Event> dbSet,
            int identity) : base(conformitContext, dbSet, identity)
        {
        }
    }
}
