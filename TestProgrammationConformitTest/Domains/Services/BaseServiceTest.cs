using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformitTest.Domains.Services
{
    public class BaseServiceTest
    {
        protected ConformitContext ConformitContext;

        protected void Setup()
        {
            var defaultDatabaseName = typeof(ConformitContext).FullName;
            var databaseName = null == defaultDatabaseName ? "Test" : $"{defaultDatabaseName}-Test";
            var options = new DbContextOptionsBuilder<ConformitContext>().UseInMemoryDatabase(databaseName).Options;

            ConformitContext = new ConformitContext(options, new Env());
        }
    }
}
