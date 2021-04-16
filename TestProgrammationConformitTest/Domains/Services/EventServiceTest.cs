using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;

namespace TestProgrammationConformitTest.Domains.Services
{
    /// <summary>
    /// EventService test cases
    /// </summary>
    public class EventServiceTest : BaseServiceTest
    {
        private IService<Event, int> _eventService;
        private IService<Stakeholder, int> _stakeholderService;

        [SetUp]
        public new void Setup()
        {
            _eventService = new EventService(ConformitContext, ConformitContext.Events, 0);
            _stakeholderService = new StakeholderService(ConformitContext, ConformitContext.Stakeholders, 0);
        }

        [Test]
        public void Persist_WithEventIdless_ReturnsAnEvent()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var @event = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.IsInstanceOf<Event>(@event);
        }

        [Test]
        public void Persist_WithIdlessEvent_RecordASingleEvent()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            var count = ConformitContext.Events.Count();
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Persist_WithIdlessEvent_ReturnsAnEventWithSameTitle()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var title = Guid.NewGuid().ToString();
            var @event = _eventService.Persist(new Event
            {
                Title = title,
                StakeholderId = stakeholderId,
                Description = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(title, @event?.Title);
        }

        [Test]
        public void Persist_WithIdlessEvent_ReturnsAnEventWithSameDescription()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var description = Guid.NewGuid().ToString();
            var @event = _eventService.Persist(new Event
            {
                Description = description,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(description, @event?.Description);
        }

        [Test]
        public void Persist_WithIdlessEvent_ReturnsAnEventWithSameStakeholderId()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var @event = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(stakeholderId, @event?.StakeholderId);
        }

        [Test]
        public void Persist_WithIdlessEvent_ReturnsAnEventWithNonNullId()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            Assert.AreNotEqual(0, eventId);
        }

        [Test]
        public void Persist_WithEvent_ReturnsAnEvent()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            var @event = _eventService.Persist(new Event
            {
                Id = eventId,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.IsInstanceOf<Event>(@event);
        }

        [Test]
        public void Persist_WithEvent_RecordNoNewEvent_WhenIdMatchesExistingOne()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            Assert.AreEqual(1, ConformitContext.Events.Count());

            stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            _eventService.Persist(new Event
            {
                Id = eventId,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(1, ConformitContext.Events.Count());
        }

        [Test]
        public void Persist_WithEvent_ReturnsAnEventWithSameId_WhenIdMatchesExistingOne()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            var @event = _eventService.Persist(new Event
            {
                Id = eventId,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(eventId, @event?.Id);
        }

        [Test]
        public void Persist_WithEvent_ReturnsAnEventWithNewDescription_WhenIdMatchesExistingOne()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;

            stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var description = Guid.NewGuid().ToString();
            var @event = _eventService.Persist(new Event
            {
                Id = eventId,
                Description = description,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(description, @event?.Description);
        }

        [Test]
        public void Persist_WithEvent_ReturnsAnEventWithNewStakeholderId_WhenIdMatchesExistingOne()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;

            stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var @event = _eventService.Persist(new Event
            {
                Id = eventId,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.AreEqual(stakeholderId, @event?.StakeholderId);
        }

        [Test]
        public void Persist_WithEvent_ReturnsAnEventWithNonNullId_WhenIdMatchesExistingOne()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;

            stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var @event = _eventService.Persist(new Event
            {
                Id = eventId,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.AreNotEqual(0, @event?.Id);
        }

        [Test]
        public void Persist_WithStakeholderHavingUnknownId_ReturnsNull()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var @event = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Id = new Random().Next(0, int.MaxValue),
            })!.Id;
            Assert.AreEqual(null, @event);
        }

        [Test]
        public void Find_WithTId_ReturnsNullIfNoneWithMatchingId()
        {
            var @event = _eventService.Find(new Random().Next(1, int.MaxValue));
            Assert.IsNull(@event);
        }

        [Test]
        public void Find_WithTId_ReturnsAnEvent()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var @event = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            Assert.IsInstanceOf<Event>(@event);
        }

        [Test]
        public void FindT_WithId_ReturnsAnEventWithSameId()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            var @event = _eventService.Find(eventId);
            Assert.AreEqual(eventId, @event!.Id);
        }

        [Test]
        public void Find_WithTId_ReturnsAnEventWithPersistedTitle()
        {
            var title = Guid.NewGuid().ToString();
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                Title = title,
                StakeholderId = stakeholderId,
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            var @event = _eventService.Find(eventId);
            Assert.AreEqual(title, @event!.Title);
        }

        [Test]
        public void Find_WithTId_ReturnsAnEventWithPersistedDescription()
        {
            var description = Guid.NewGuid().ToString();
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                Description = description,
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
            })!.Id;
            var @event = _eventService.Find(eventId);
            Assert.AreEqual(description, @event!.Description);
        }

        [Test]
        public void Find_WithTId_ReturnsAnEventWithPersistedStakeholderId()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            var @event = _eventService.Find(eventId);
            Assert.AreEqual(stakeholderId, @event!.StakeholderId);
        }

        [Test]
        public void Find_WithPageAndSize_ReturnsEmptyEnumerable_WhenTheDatasetIsEmpty()
        {
            Assert.IsEmpty(_eventService.Find(1, 10));
        }

        [Test]
        public void Find_WithPageAndSize_ReturnsEmptyEnumerable_WhenSizeIsZero()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            });
            Assert.IsEmpty(_eventService.Find(0, 0));
            Assert.IsEmpty(_eventService.Find(1, 0));
        }

        [Test]
        public void Find_WithPageAndSize_ReturnsAtMostSizeEnumerable()
        {
            for (var i = 0; i < 3; i++)
            {
                var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
                _eventService.Persist(new Event
                {
                    StakeholderId = stakeholderId,
                    Title = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString(),
                });
            }

            var events = _eventService.Find(1, 2);
            Assert.AreEqual(2, events.Count());

            events = _eventService.Find(2, 2);
            Assert.AreEqual(1, events.Count());
        }

        [Test]
        public void Find_WithPageAndSize_CanBeUsedToIterateOverTheCompleteDataset()
        {
            var controlIds = new HashSet<int>();
            var ids = new HashSet<int>();
            int page = 1, count = 0;

            for (var i = 0; i < 5; i++)
            {
                var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
                var eventId = _eventService.Persist(new Event
                {
                    StakeholderId = stakeholderId,
                    Title = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString(),
                })!.Id;
                controlIds.Add(eventId);
            }

            do
            {
                var events = _eventService.Find(page++, 2).ToList();
                count = events.Count;

                foreach (var @event in events)
                {
                    ids.Add(@event.Id);
                }
            } while (count > 0);

            Assert.AreEqual(controlIds.Count, ids.Count);

            foreach (var controlId in controlIds)
            {
                Assert.True(ids.Contains(controlId));
            }
        }

        [Test]
        public void Delete_WithTId_ReturnsFalse_WhenTIdMatchesNotDatasetEntry()
        {
            Assert.False(_eventService.Delete(new Random().Next(0, int.MaxValue)));
        }

        [Test]
        public void Delete_WithTId_ReturnsTrue_WhenMatchingEntryHasSuccessfullyBeenDeleted()
        {
            var stakeholderId = _stakeholderService.Persist(new Stakeholder {Name = Guid.NewGuid().ToString()})!.Id;
            var eventId = _eventService.Persist(new Event
            {
                StakeholderId = stakeholderId,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            })!.Id;
            Assert.True(_eventService.Delete(eventId));
            Assert.AreEqual(0, ConformitContext.Events.Count());
        }
    }
}
