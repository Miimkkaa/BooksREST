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

        [TestMethod]
        public void TestPosting()
        {
            Person newPerson = new Person
            {
                FullName = "Kirilka Kirova",
                Username = "kiki",
                Pass = "123456789",
                Email = "kiki@gmail.com",
                Type = 1
            };
            bool perCount = _controller.PostAsync(newPerson);
            Assert.AreEqual(true, perCount);

            IEnumerable<Person> personList = _controller.Get();
            Assert.AreEqual(3, personList.Count()); // Passed
        }

        [TestMethod]
        public void TestPutPerson()
        {
            Person newPerson = new Person()
            {
                FullName = "Milena Ognianova",
                Email = "mimka@gmail.bg"
            };
            int usersCount = _controller.Put(2, newPerson);
            Assert.AreEqual("12345", newPerson.Email); // Passed
        }

        public void TestPutPass()
        {
            Person newPerson = new Person()
            {
                Pass = "M123456",
            };
            int usersCount = _controller.Put(2, newPerson);
            Assert.AreEqual("123456", newPerson.Pass); // Passed
        }
    }
}
