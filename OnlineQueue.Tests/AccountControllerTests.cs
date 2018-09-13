using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplication2.Controllers;
using Xunit;

namespace OnlineQueue.Tests
{
    public class AccountControllerTests
    {
        DBContext db = new DBContext();
        Mock<IStringLocalizer<AccountController>> moqLoc = new Mock<IStringLocalizer<AccountController>>();
        Mock<ILogger<AccountController>> moqLog = new Mock<ILogger<AccountController>>();

        [Fact]
        public void LoginGetTest()
        {
            var context = db.GetContextWithData();
            var controller = new AccountController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Login() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Login");
        }

        [Fact]
        public void LoginPostTest()
        {
            var context = db.GetContextWithData();
            var controller = new AccountController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Login(null).Result as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Login");
        }

        [Fact]
        public void RegisterGetTest()
        {
            var context = db.GetContextWithData();
            var controller = new AccountController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Register() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Register");
        }

        [Fact]
        public void RegisterPostTest()
        {
            var context = db.GetContextWithData();
            var controller = new AccountController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Register(null).Result as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Register");
        }

        [Fact]
        public void SettingAccountGetTest()
        {
            var context = db.GetContextWithData();
            var controller = new AccountController(context, moqLoc.Object, moqLog.Object);

            var result = controller.SettingAccount() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "SettingAccount");
        }

        [Fact]
        public void SettingsAccountPostTest()
        {
            var context = db.GetContextWithData();
            var controller = new AccountController(context, moqLoc.Object, moqLog.Object);

            var result = controller.SettingAccount(null).Result as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "SettingAccount");
        }
    }
}
