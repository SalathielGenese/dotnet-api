using System.Linq;
using NUnit.Framework;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformitTest.Domains.Services
{
    public class StakeholderServiceTest : BaseServiceTest
    {
        private StakeholderService _stakeholderService;

        [SetUp]
        public new void Setup()
        {
            base.Setup();
            _stakeholderService = new StakeholderService(ConformitContext, ConformitContext.Stakeholders, 0);
        }

        [Test]
        public void Persist_WithStakeholderDto_ReturnsAStakeholder()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            Assert.IsInstanceOf(typeof(Stakeholder), stakeholder);
        }

        [Test]
        public void Persist_WithStakeholderDto_RecordASingleStakeholder()
        {
            _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var count = ConformitContext.Stakeholders.Count();
            Assert.AreEqual(count, 1);
        }

        [Test]
        public void Persist_WithStakeholderDto_ReturnsAStakeholderWithSameName()
        {
            var name = "Shaban";
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = name});
            Assert.AreEqual(stakeholder?.Name, name);
        }

        [Test]
        public void Persist_WithStakeholderDto_ReturnsAStakeholderWithNonNullId()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            Assert.AreNotEqual(stakeholder?.Id, 0);
        }
    }
}
