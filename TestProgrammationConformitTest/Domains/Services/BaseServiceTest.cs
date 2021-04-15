using System;
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
            var contextBuilder = new DbContextOptionsBuilder<ConformitContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = contextBuilder.Options;

            ConformitContext = new ConformitContext(options, new Env());
        }

        [TearDown]
        protected void TearDown()
        {
            ConformitContext?.Dispose();
        }
    }
}
