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
    public class BookOrderController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/BookOrder
        [HttpGet]
        public IEnumerable<BookOrder> Get()
        {
            string selectString = "select * from ORDERBOOK;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<BookOrder> result = new List<BookOrder>();
                        while (reader.Read())
                        {
                            BookOrder item = ReadItem(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        private BookOrder ReadItem(SqlDataReader reader)
        {
            int boId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            int orderId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            int bookId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            int cardNumber = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
            DateTime expiryDate = reader.IsDBNull(4) ? DateTime.Parse("1754-11-11T00:00:00.00") : reader.GetDateTime(4);
            int cvv = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);

            BookOrder item = new BookOrder()
            {
                BOId = boId,
                OrderId = orderId,
                Bookid = bookId,
                CardNumber = cardNumber,
                ExpiryDate = expiryDate,
                CVV = cvv
            };

            return item;
        }

        // GET: api/Order/5
        //[HttpGet("{id}", Name = "Get")]
        [Route("{id}")]
        public BookOrder Get(int id)
        {
            try
            {
                string selectString = "select * from ORDERBOOK where OBId = @id";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectString, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return ReadItem(reader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //future handling exceptions
                return null;
            }
        }

        //get all books for 1 order
        [Route("personOrder/{orderId}")]
        public List<Book> GetPersonOrder(int orderId)
        {
                string selectString = "select * from ORDERBOOK where OrdersId = @orderId";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectString, conn))
                    {
                        command.Parameters.AddWithValue("@orderId", orderId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Book> result = new List<Book>();
                            while (reader.Read())
                            {
                                BookOrder item = ReadItem(reader);
                                result.Add(GetBook(item.Bookid));
                        }
                        return result;
                        }
                    }
                }
        }

        private Book ReadBook(SqlDataReader reader)
        {
            int bookId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            string title = reader.IsDBNull(1) ? "" : reader.GetString(1);
            string author = reader.IsDBNull(2) ? "" : reader.GetString(2);
            string bookDes = reader.IsDBNull(3) ? "" : reader.GetString(3);
            string genre = reader.IsDBNull(4) ? "" : reader.GetString(4);
            decimal price = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
            int nop = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
            int nov = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
            string bookPdf = reader.IsDBNull(8) ? "" : reader.GetString(8);
            string coverPhoto = reader.IsDBNull(9) ? "" : reader.GetString(9);

            Book item = new Book()
            {
                BookId = bookId,
                Title = title,
                Author = author,
                BookDes = bookDes,
                Genre = genre,
                Price = price,
                NoP = nop,
                NoV = nov,
                BookPdf = bookPdf,
                CoverPhoto = coverPhoto
            };

            return item;
        }

        // GET: api/Order/5
        public Book GetBook(int id)
        {
            try
            {
                string selectString = "select * from BOOK where BookId = @id";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectString, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return ReadBook(reader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //future handling exceptions
                return null;
            }
        }

        // POST: api/BookOrder
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/BookOrder/5
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
