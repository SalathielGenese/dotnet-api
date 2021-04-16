using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestProgrammationConformit.Controllers;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;
using TestProgrammationConformit.Infrastructures.Http;

namespace TestProgrammationConformitTest.Controllers
{
    public class StakeholderUnitTest
    {
        private BaseController<Stakeholder, int> _stakeholderController;
        private Mock<IService<Stakeholder, int>> _stakeholderServiceMock;

        [SetUp]
        public void SetUp()
        {
            _stakeholderServiceMock = new Mock<IService<Stakeholder, int>>();
            _stakeholderController = new StakeholderController(_stakeholderServiceMock.Object);
        }

        [Test]
        public void Index_CallStakeholderServiceFindPageSizeOnce()
        {
            _stakeholderController.Index(new Pageable());
            _stakeholderServiceMock.Verify(
                service => service.Find(
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Once);
        }

        [Test]
        public void Index_CallStakeholderServiceFindPageSizeWithGivenPage()
        {
            var page = new Random().Next();
            _stakeholderController.Index(new Pageable {Page = page});
            _stakeholderServiceMock.Verify(
                service => service.Find(
                    It.Is<int>(value => value == page),
                    It.IsAny<int>()),
                Times.Once);
        }

        [Test]
        public void Index_CallStakeholderServiceFindPageSizeWithGivenSize()
        {
            var size = new Random().Next();
            _stakeholderController.Index(new Pageable {Size = size});
            _stakeholderServiceMock.Verify(
                service => service.Find(
                    It.IsAny<int>(),
                    It.Is<int>(value => value == size)),
                Times.Once);
        }

        [Test]
        public void Index_ReturnAnActionResultOfEnumerableStakeholders()
        {
            Assert.IsInstanceOf<ActionResult<IEnumerable<Stakeholder>>>(_stakeholderController.Index(new Pageable()));
        }

        [Test]
        public void Index_ReturnAnActionResultOfWhichResultIsOkObjectResult()
        {
            Assert.IsInstanceOf<OkObjectResult>(_stakeholderController.Index(new Pageable()).Result);
        }

        [Test]
        public void Index_ReturnAnActionResultContainingHttpOkStatusCode()
        {
            var result = _stakeholderController.Index(new Pageable()).Result as OkObjectResult;
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void Index_ReturnAnActionResultContainingServiceReturnValue()
        {
            var stakeholders = new List<Stakeholder>();
            _stakeholderServiceMock.Setup(service =>
                service.Find(It.IsAny<int>(), It.IsAny<int>())
            ).Returns(stakeholders);
            var result = _stakeholderController.Index(new Pageable()).Result as OkObjectResult;
            Assert.AreSame(stakeholders, result?.Value);
        }

        // TODO: Continue this way...
    }
}
