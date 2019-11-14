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
    public class BookController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/Book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            string selectString = "select * from BOOK;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Book> result = new List<Book>();
                        while (reader.Read())
                        {
                            Book item = ReadItem(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        private Book ReadItem(SqlDataReader reader)
        {
            int bookId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            string title = reader.IsDBNull(1) ? "" : reader.GetString(1);
            string author = reader.IsDBNull(2) ? "" : reader.GetString(2);
            string bookDes = reader.IsDBNull(3) ? "" : reader.GetString(3);
            string genre = reader.IsDBNull(4) ? "" : reader.GetString(4);
            double price = reader.IsDBNull(5) ? 0 : reader.GetDouble(5);
            int nop = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
            int nov = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);

            Book item = new Book()
            {
                BookId = bookId,
                Title = title,
                Author = author,
                BookDes = bookDes,
                Genre = genre,
                Price = price,
                NoP = nop,
                NoV = nov
            };

            return item;
        }

        // GET: api/Order/5
        //[HttpGet("{id}", Name = "Get")]
        [Route("{id}")]
        public Book Get(int id)
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

        // POST: api/Book
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Book/5
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
