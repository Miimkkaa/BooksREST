using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BooksTry.Controllers;
using BooksTry.Models;
using System.Linq;

namespace BooksTesting
{
    [TestClass]
    public class OrderedBooksControllerTest
    {
        private readonly OrderedBooksController _controller = new OrderedBooksController();

        [TestMethod]
        public void TestMethod()
        {
            IEnumerable<OrderedBooks> list = _controller.Get();
            Assert.AreEqual(3, list.Count()); //passed

            IEnumerable<OrderedBooks> orderedBooksByPersonId = _controller.Get(2); //Get by id
            Assert.AreEqual(1, orderedBooksByPersonId.Count()); //passed
        }
    }
}
