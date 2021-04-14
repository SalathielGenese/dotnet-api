using Microsoft.EntityFrameworkCore;

namespace TestProgrammationConformit.Infrastructures
{
    public class ConformitContext : DbContext
    {
        public ConformitContext(DbContextOptions options, Env env) : base(options)
        {
            Env = env;
        }

        private Env Env { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Env.PostgresConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}