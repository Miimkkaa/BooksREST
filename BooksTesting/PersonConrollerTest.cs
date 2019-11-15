using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BooksTry.Controllers;
using BooksTry.Models;
using System.Linq;

namespace BooksTesting
{
    [TestClass]
    public class PersonConrollerTest
    {
        private readonly PersonController _controller = new PersonController();

        [TestMethod]
        public void TestGetAll()
        {
            IEnumerable<Person> persons = _controller.Get();
            Assert.AreEqual(3, persons.Count());

            Person person = _controller.Get(1); //get by id
            Assert.AreEqual("mimka", person.Username);
        }
    }
}
