using System;
using NUnit;
using NUnit.Framework;
using Moq;
using license_manager;

namespace license_manager.Tests
{
    [TestFixture]
    public class SettingTests
    {
        //[Test]
        //public void DbSettingTest()
        //{
        //    // Arrange
        //    var mockDependency = new Mock<ISettingsRepository>();

        //    mockDependency.Setup(x => x.Get()).Returns(() => new SettingsRepository
        //    {
        //         EmailHost = "host",
        //         EmailPort = 443,
        //         EmailUsername ="usr",
        //         EmailPassword ="pass",
        //         Email = "i@o2.pl",
        //         EmailFromName = "test",
        //         EmailSubjectGroupInvitation = "subject",
        //         EmailBodyGroupInvitation = "<html></html>"
        //    });

        //    // Act
        //    var mailClass = new MailClass()
        //    {
        //        SettingsRepository = mockDependency.Object
        //    };
            
        //    // Assert
        //    var res =  mailClass?.GetSettings();
        //    if (res != null)
        //    {
        //        Assert.AreEqual("hots", res.EmailHost);
        //        Assert.AreNotEqual(423, res.EmailPort);
        //        Assert.NotNull(res.Email);
        //        Assert.AreEqual("i@o2.pl", res.Email);

        //        Assert.AreEqual("test", res.EmailFromName);
        //        Assert.AreEqual("<html></html>", res.EmailBodyGroupInvitation);
        //    }
        //    else
        //    {
        //        Assert.AreEqual(1,2);
        //    }
        //}
    }
}
