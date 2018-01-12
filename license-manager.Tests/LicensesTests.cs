using System;
using System.Linq;
using licensemanager.Controllers;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories;
using licensemanager.Repositories.Interfaces;
using NUnit;
using NUnit.Framework;
using Moq;

namespace license_manager.Tests
{
    [TestFixture]
    public class LicensesTests
    {
        [Test]
        public void GetLicenseByIdTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();

            mockDependency.Setup(x => x.GetById(1)).Returns(() => new LicenseModel
            {
                Id = 1,
                IdClient = 2,
                ClientName = "testClientName",
                IsActive = true,
                IdApplication = 1,
                Number = "XXXX-XXXX-XXXX-XXXX"
            });

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller?.GetById(1);
            if (res?.Data != null)
            {
                Assert.AreEqual("OK", res.Description);
                Assert.AreEqual("testClientName", res.Data.ClientName);
                Assert.AreEqual(true, res.Data.IsActive);
                Assert.AreEqual("XXXX-XXXX-XXXX-XXXX", res.Data.Number);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [Test]
        public void PutLicense()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            var model = new LicenseModel()
            {
                ClientName = "test",
                Id = 5,
                IdApplication = 4
            };

            mockDependency.Setup(x => x.EditLicense(5, model)).Returns(() => true);

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.Put(model);
            if (res?.Data != null)
            {
                Assert.AreEqual("OK", res.Description);
                Assert.AreEqual(true, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [Test]
        public void PutLicenseWithoutIdTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            var model = new LicenseModel()
            {
                ClientName = "test"
            };

            mockDependency.Setup(x => x.EditLicense(1, model)).Returns(() => true);

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.Put(model);
            if (res?.Data != null)
            {
                Assert.AreEqual("Error: Id license <= 0", res.Description);
                Assert.AreEqual(false, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [Test]
        public void PutLicenseWithoutDataTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            LicenseModel model = null;

            mockDependency.Setup(x => x.EditLicense(1, model)).Returns(() => true);

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.Put(model);
            if (res?.Data != null)
            {
                Assert.AreEqual("Error: Data is null", res.Description);
                Assert.AreEqual(false, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [Test]
        public void PostReturnIdLicenseWithoutIdClientTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            var modelLicenseModel = new LicenseModel()
            {
                ClientName = "test"
            };
            var modelLicenses = new Licenses()
            {
                Number = "XXX-XXX-XXX-XXX"
            };
           
            mockDependency.Setup(x => x.Insert(modelLicenses)).Returns(() => true);

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.PostReturnId(modelLicenseModel);
            if (res?.Data != null)
            {
                Assert.AreEqual("Id Client is required", res.Description);
                Assert.AreEqual(0, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [Test]
        public void PostReturnIdLicenseTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            var modelLicenseModel = new LicenseModel()
            {
                ClientName = "test",
                IdClient =1,
                IdApplication = 5
            };
            var modelLicenses = new Licenses()
            {
                Number = "XXX-XXX-XXX-XXX",
                Id = 0,
            };
            
            mockDependency.Setup(x => x.Insert(modelLicenses)).Returns(()=>(bool)true);

            // Act
            var controller = new LicenseController
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.PostReturnId(modelLicenseModel);
            if (res?.Data != null)
            {
                Assert.AreEqual(modelLicenses.Id, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }

        [Test]
        public void PostReturnIdErrorIdAppTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            var modelLicenseModel = new LicenseModel()
            {
                ClientName = "test",
                IdClient = 1,
            };
            var modelLicenses = new Licenses()
            {
                Number = "XXX-XXX-XXX-XXX",
                Id = 1,
            };

            mockDependency.Setup(x => x.Insert(modelLicenses)).Returns(() => true);

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.PostReturnId(modelLicenseModel);
            if (res?.Data != null)
            {
                Assert.AreEqual("Id Application is required", res.Description);
                Assert.AreEqual(0, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }
        [Test]
        public void PostReturnIdErrorIdClientTest()
        {
            var mockDependency = new Mock<ILicenseRepository>();
            var modelLicenseModel = new LicenseModel()
            {
                ClientName = "test",
            };
            var modelLicenses = new Licenses()
            {
                Number = "XXX-XXX-XXX-XXX",
                Id = 1,
            };

            mockDependency.Setup(x => x.Insert(modelLicenses)).Returns(() => true);

            // Act
            var controller = new LicenseController()
            {
                AppRepo = mockDependency.Object
            };

            // Assert
            var res = controller.PostReturnId(modelLicenseModel);
            if (res?.Data != null)
            {
                Assert.AreEqual("Id Client is required", res.Description);
                Assert.AreEqual(0, res.Data);
            }
            else
            {
                Assert.AreEqual(1, 2);
            }
        }
    }
}
