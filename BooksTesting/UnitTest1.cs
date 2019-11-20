using System.Collections.Generic;
using System.Linq;
using BooksTry.Controllers;
using BooksTry.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooksTesting
{
    [TestClass]
    public class UnitTest1
    {
        private readonly BookController _controller = new BookController();

        [TestMethod]
        public void TestGetAll()
        {
            IEnumerable<Book> books = _controller.Get();
            Assert.AreEqual(24, books.Count());

            Book book = _controller.Get(8); //Get by id
            Assert.AreEqual("Moby Dick", book.Title);
        }


    }
}
