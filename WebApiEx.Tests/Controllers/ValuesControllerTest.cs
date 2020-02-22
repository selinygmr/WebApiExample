using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using BussinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiEx;
using WebApiEx.Controllers;

namespace WebApiEx.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            Mock<ICurrencyProvider> mockProvider1 = new Mock<ICurrencyProvider>();
            mockProvider1.Setup(x => x.GetRate("usd")).Returns(4.1267M);

            Mock<ICurrencyProvider> mockProvider2 = new Mock<ICurrencyProvider>();
            mockProvider2.Setup(x => x.GetRate("usd")).Returns(4.0001M);

            // Act
            BussinessService bussinessService = new BussinessService();
            bussinessService.AddCurrencyProvider(mockProvider1.Object);
            bussinessService.AddCurrencyProvider(mockProvider2.Object);
            var actual = bussinessService.GetMinRate("usd");

            // Assert
            var expected = 4.0001M;
            Assert.AreEqual(expected, actual);
        }
    }
}
