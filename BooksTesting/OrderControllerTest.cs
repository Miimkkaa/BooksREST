using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BooksTry.Controllers;
using BooksTry.Models;
using System.Linq;

namespace BooksTesting
{
    [TestClass]
    public class OrderControllerTest
    {
        private readonly OrderController _controller = new OrderController();

        [TestMethod]
        public void TestMethod()
        {
            IEnumerable<Order> list = _controller.Get();
            Assert.AreEqual(3, list.Count()); //passed

            Order item = _controller.Get(2); //Get by id
            Assert.AreEqual(true, item.Paid); //passed
        }
    }
}
