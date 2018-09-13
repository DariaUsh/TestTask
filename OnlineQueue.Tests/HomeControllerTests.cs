using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication2;
using WebApplication2.Controllers;
using Xunit;

namespace OnlineQueue.Tests
{
    public class HomeControllerTests
    {
        DBContext db = new DBContext();
        Mock<IStringLocalizer<HomeController>> moqLoc = new Mock<IStringLocalizer<HomeController>>();
        Mock<ILogger<HomeController>> moqLog = new Mock<ILogger<HomeController>>();

        [Fact]
        public void IndexTest()
        {
            var context = db.GetContextWithData();
            var controller = new HomeController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void AddInLineTest()
        {
            var context = db.GetContextWithData();
            var controller = new HomeController(context, moqLoc.Object, moqLog.Object);

            var result = controller.AddInLine() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void RemoveInLineTest()
        {
            var context = db.GetContextWithData();
            var controller = new HomeController(context, moqLoc.Object, moqLog.Object);

            var result = controller.RemoveInLine() as PartialViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_Message");
        }

        [Fact]
        public void LineTest()
        {
            var context = db.GetContextWithData();
            var controller = new HomeController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Line() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Line");
        }

        [Fact]
        public void CompleteTest()
        {
            var context = db.GetContextWithData();
            var controller = new HomeController(context, moqLoc.Object, moqLog.Object);

            var result = controller.Complete() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void UpdateStatusTest()
        {
            var context = db.GetContextWithData();
            var controller = new HomeController(context, moqLoc.Object, moqLog.Object);

            var result = controller.UpdateStatus() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }


    }
}
