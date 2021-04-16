using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.Domains.Services
{
    public class CommentService: BaseService<Comment, int>
    {
        public CommentService(
            ConformitContext conformitContext,
            DbSet<Comment> dbSet,
            int identity) : base(conformitContext, dbSet, identity)
        {
        }
    }
}
