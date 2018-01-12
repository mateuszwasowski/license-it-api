using System;
using System.Linq;
using licensemanager.Controllers;
using licensemanager.Models;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories;
using NUnit;
using NUnit.Framework;
using Moq;
using licensemanager.Repositories.Interfaces;

namespace license_manager.Tests
{
    [TestFixture]
    public class ApplicationsTests
    {
        [Test]
        public void GetApplicationModelByIdTest()
        {
            // Arrange
            var mockDependency = new Mock<IApplicationsRepository>();

            mockDependency.Setup(x => x.GetApplicationModelById(1)).Returns(() => new ApplicationModel
            {
                    Description = "test",
                    Hash = "#&^Grfefsdf"
            });

            // Act
            var controller = new ApplicationsController()
            {
                ApplicationsRepository = mockDependency.Object
            };
            
            // Assert
            var res = controller?.GetById(1);
            if (res?.Data != null)
            {
                Assert.AreEqual("test", res.Data.Description);
                Assert.AreEqual("#&^Grfefsdf", res.Data.Hash);
                Assert.AreEqual("OK", res.Description);
            }
            else
            {
                Assert.AreEqual(1,2);
            }
        }
    }
}
