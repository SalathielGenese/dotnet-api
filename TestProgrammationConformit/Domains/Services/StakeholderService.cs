using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.Domains.Services
{
    public class StakeholderService : BaseService<Stakeholder, int>
    {
        public StakeholderService(
            ConformitContext conformitContext,
            DbSet<Stakeholder> dbSet,
            int identity) : base(conformitContext, dbSet, identity)
        {
        }
    }
}
