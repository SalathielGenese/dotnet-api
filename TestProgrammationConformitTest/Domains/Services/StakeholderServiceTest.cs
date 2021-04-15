using System;
using System.Collections.Generic;
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
            var id = new Random().Next(0, int.MaxValue);

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

        [Test]
        public void Find_WithPageAndSize_ReturnsEmptyEnumerable_WhenTheDatasetIsEmpty()
        {
            var stakeholders = _stakeholderService.Find(1, 10);
            Assert.IsEmpty(stakeholders);
        }

        [Test]
        public void Find_WithPageAndSize_ReturnsEmptyEnumerable_WhenPageIsZeroAndSizeIsZero()
        {
            _stakeholderService.Persist(new Stakeholder {Name = "Shaban"});
            var stakeholders = _stakeholderService.Find(0, 0);
            Assert.IsEmpty(stakeholders);
        }

        [Test]
        public void Find_WithPageAndSize_ReturnsAtMostSizeEnumerable()
        {
            _stakeholderService.Persist(new Stakeholder {Name = "Irban"});
            _stakeholderService.Persist(new Stakeholder {Name = "Harban"});
            _stakeholderService.Persist(new Stakeholder {Name = "Sokoban"});

            var stakeholders = _stakeholderService.Find(1, 2);
            Assert.AreEqual(2, stakeholders.Count());

            stakeholders = _stakeholderService.Find(2, 2);
            Assert.AreEqual(1, stakeholders.Count());
        }

        [Test]
        public void Find_WithPageAndSize_CanBeUsedToIterateOverTheCompleteDataset()
        {
            var names = new HashSet<string>(new[] {"Kublai", "Khan", "Ozenu", "Mephisthos"});
            List<Stakeholder> stakeholders = new List<Stakeholder>();
            var ids = new HashSet<int>();
            int page = 1, count = 0;

            foreach (var name in names)
            {
                _stakeholderService.Persist(new Stakeholder {Name = name});
            }

            do
            {
                var result = _stakeholderService.Find(page++, 2).ToList();
                count = result.Count;

                foreach (var stakeholder in result)
                {
                    stakeholders.Add(stakeholder);
                    ids.Add(stakeholder.Id);
                }
            } while (0 < count);

            Assert.AreEqual(names.Count, ids.Count);
            Assert.AreEqual(names.Count, stakeholders.Count);

            foreach (var name in names)
            {
                Assert.IsTrue(stakeholders.Any(stakeholder => name.Equals(stakeholder.Name)));
            }
        }

        [Test]
        public void Delete_WithTId_ReturnsFalse_WhenTIdMatchesNotDatasetEntry()
        {
            var deleted = _stakeholderService.Delete(new Random().Next(0, int.MaxValue));
            Assert.False(deleted);
        }

        [Test]
        public void Delete_WithTId_ReturnsTrue_WhenMatchingEntryHasSuccessfullyBeenDeleted()
        {
            var persisted = _stakeholderService.Persist(new Stakeholder {Name = "Dataktar"});
            var deleted = _stakeholderService.Delete(persisted!.Id);
            Assert.True(deleted);
        }
    }
}
