using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Api;
using Xunit.Sdk;

using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class TestWebApiControllerTests
    {
        private TestWebApiController _Controller;

        [TestInitialize]
        public void Initialize()
        {
            var value_service_mock = new Mock<IValuesService>();

            value_service_mock
               .Setup(service => service.GetAsync())
               .ReturnsAsync(new [] { "1", "2" });

            _Controller = new TestWebApiController(value_service_mock.Object);
        }

        [TestMethod]
        public async Task Index_Method_Returns_View_With_Values()
        {
            var result = await _Controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);
            const int expected_count = 2;
            Assert.Equal(expected_count, model.Count());
        }
    }
}
