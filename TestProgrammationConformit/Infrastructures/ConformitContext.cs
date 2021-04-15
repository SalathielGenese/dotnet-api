using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Domains.Models;

namespace TestProgrammationConformit.Infrastructures
{
    public class ConformitContext : DbContext
    {
        public ConformitContext(DbContextOptions options, Env env) : base(options)
        {
            Env = env;
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stakeholder> Stakeholders { get; set; }

        private Env Env { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(Env.PostgresConnectionString))
            {
                optionsBuilder.UseNpgsql();
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
