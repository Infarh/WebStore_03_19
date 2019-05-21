using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _Controller;

        [TestInitialize]
        public void Initialize()
        {
            _Controller = new HomeController();
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var result = _Controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_Redirect_to_NotFound()
        {
            var result = _Controller.ErrorStatus("404");
            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect_to_action.ControllerName);
            Assert.Equal(nameof(HomeController.NotFoundPage), redirect_to_action.ActionName);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _Controller.Blog();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _Controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Index_Returns_View()
        {
            var result = _Controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void NotFoundPage_Returns_View()
        {
            var result = _Controller.NotFoundPage();
            Assert.IsType<ViewResult>(result);
        }
    }
}
