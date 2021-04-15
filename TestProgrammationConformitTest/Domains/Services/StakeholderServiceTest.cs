using System;
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
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Persist_WithStakeholderDto_ReturnsAStakeholderWithSameName()
        {
            var name = "Shaban";
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = name});
            Assert.AreEqual(name, stakeholder?.Name);
        }

        [Test]
        public void Persist_WithStakeholderDto_ReturnsAStakeholderWithNonNullId()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            Assert.AreNotEqual(0, stakeholder?.Id);
        }

        [Test]
        public void Persist_WithStakeholder_ReturnsAStakeholder()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            stakeholder = _stakeholderService.Persist(stakeholder!);
            Assert.IsInstanceOf(typeof(Stakeholder), stakeholder);
        }

        [Test]
        public void Persist_WithStakeholder_RecordNoNewStakeholder()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            Assert.AreEqual(1, ConformitContext.Stakeholders.Count());
            _stakeholderService.Persist(stakeholder!);
            Assert.AreEqual(1, ConformitContext.Stakeholders.Count());
        }

        [Test]
        public void Persist_WithStakeholder_ReturnsAStakeholderWithSameName()
        {
            var name = "Shaban";
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = name});
            stakeholder = _stakeholderService.Persist(stakeholder!);
            Assert.AreEqual(name, stakeholder?.Name);
        }

        [Test]
        public void Persist_WithStakeholder_ReturnsAStakeholderWithNonNullId()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            stakeholder = _stakeholderService.Persist(stakeholder!);
            Assert.AreNotEqual(0, stakeholder?.Id);
        }

        [Test]
        public void Persist_WithStakeholder_ReturnsAStakeholderWithSameId()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var id = stakeholder!.Id;
            stakeholder = _stakeholderService.Persist(stakeholder!);
            Assert.AreEqual(id, stakeholder?.Id);
        }

        [Test]
        public void Persist_WithStakeholderHavingUpdatedName_ReturnsAStakeholderWithUpdatedName()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var id = stakeholder!.Id;
            var name = "Ultron";

            stakeholder.Name = name;
            stakeholder = _stakeholderService.Persist(stakeholder!);
            Assert.AreEqual(name, stakeholder?.Name);
        }

        [Test]
        public void Persist_WithStakeholderHavingUnknownId_ReturnsNull()
        {
            var stakeholder = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var id = 9835;

            stakeholder = _stakeholderService.Persist(new Stakeholder {Id = id, Name = stakeholder!.Name});
            Assert.AreEqual(null, stakeholder);
        }

        [Test]
        public void Find_WithTId_ReturnsNullIfNoneWithMatchingId()
        {
            var stakeholder = _stakeholderService.Find(new Random().Next(1, int.MaxValue));
            Assert.IsNull(stakeholder);
        }

        [Test]
        public void Find_WithTId_ReturnsAStakeholder()
        {
            var persisted = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var stakeholder = _stakeholderService.Find(persisted!.Id);
            Assert.IsInstanceOf<Stakeholder>(stakeholder);
        }

        [Test]
        public void FindT_WithId_ReturnsAStakeholderWithSameId()
        {
            var persisted = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var stakeholder = _stakeholderService.Find(persisted!.Id);
            Assert.AreEqual(persisted.Id, stakeholder!.Id);
        }

        [Test]
        public void Find_WithTId_ReturnsAStakeholderWithPersistedName()
        {
            var persisted = _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var stakeholder = _stakeholderService.Find(persisted!.Id);
            Assert.AreEqual(persisted.Name, stakeholder!.Name);
        }
    }
}
