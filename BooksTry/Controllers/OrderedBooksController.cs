using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BooksTry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksTry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderedBooksController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        //gets all working orders
        // GET: api/OrderedBooks
        [HttpGet]
        public IEnumerable<OrderedBooks> Get()
        {
            string selectString = "select o.OrdersId, o.TotalPrice, o.PersonId, b.BookId, b.Title, b.Author, b.Price, b.CoverPhoto " +
                "from dbo.ORDERS as o " +
                "inner join dbo.ORDERBOOK as ob " +
                "on o.OrdersId = ob.OrdersId " +
                "inner join dbo.BOOK as b " +
                "on ob.BookId = b.BookId " +
                "where o.Paid = 'false'; ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<OrderedBooks> result = new List<OrderedBooks>();
                        while (reader.Read())
                        {
                            OrderedBooks item = ReadOrderedBook(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        
        private OrderedBooks ReadOrderedBook(SqlDataReader reader)
        {
            int orderId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            decimal totalPrice = reader.IsDBNull(1)? 0 : reader.GetDecimal(1);
            int personId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            int bookId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
            string title = reader.IsDBNull(4) ? "" : reader.GetString(4);
            string author = reader.IsDBNull(5) ? "" : reader.GetString(5);
            decimal price = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
            string coverPhoto = reader.IsDBNull(7) ? "" : reader.GetString(7);

            OrderedBooks item = new OrderedBooks()
            {
                orderId = orderId,
                totalPrice = totalPrice,
                personId = personId,
                bookId = bookId,
                bookTitle = title,
                bookAuthor = author,
                bookPrice = price,
                bookCoverPhoto = coverPhoto
            };

            return item;
        }

        // GET: api/OrderedBooks/5
        [HttpGet("{id}")]
        public IEnumerable<OrderedBooks> Get(int id)
        {
            string selectString = "select o.OrdersId, o.TotalPrice, o.PersonId, b.BookId, b.Title, b.Author, b.Price, b.CoverPhoto " +
                "from dbo.ORDERS as o " +
                "inner join dbo.ORDERBOOK as ob " +
                "on o.OrdersId = ob.OrdersId " +
                "inner join dbo.BOOK as b " +
                "on ob.BookId = b.BookId " +
                "where o.Paid = 'false'and o.PersonId = '" + id +
                "'; ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<OrderedBooks> result = new List<OrderedBooks>();
                        while (reader.Read())
                        {
                            OrderedBooks item = ReadOrderedBook(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        // POST: api/OrderedBooks
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/OrderedBooks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
