using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformitTest.Domains.Services
{
    public class BaseServiceTest
    {
        protected ConformitContext ConformitContext;

        [SetUp]
        protected void Setup()
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            connection.Open();

            var builder = new DbContextOptionsBuilder<ConformitContext>()
                .UseSqlite(connection);
            var options = builder.Options;

            ConformitContext = new ConformitContext(options, new Env());
            ConformitContext?.Database.EnsureCreated();
        }

        [TearDown]
        protected void TearDown()
        {
            ConformitContext?.Database.EnsureDeleted();
        }
    }
}
