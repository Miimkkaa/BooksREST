using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BooksTry.Controllers;
using BooksTry.Models;
using System.Linq;

namespace BooksTesting
{
    [TestClass]
    public class BookOrderControllerTest
    {
        private readonly BookOrderController _controller = new BookOrderController();

        [TestMethod]
        public void TestMethod()
        {
            IEnumerable<BookOrder> list = _controller.Get();
            Assert.AreEqual(5, list.Count()); //passed

            BookOrder item = _controller.Get(1); //Get by id
            Assert.AreEqual(1, item.Bookid); //passed

            if (list.Count() == 5)
            {
                int rowsAffected = _controller.Delete(3, 1); //orderId, bookID
                Assert.AreEqual(1, rowsAffected); //passed
            }
        }
    }
}
