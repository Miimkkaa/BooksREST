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

        private Book ReadItem(SqlDataReader reader)
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

        // GET: api/Book/5
        //[HttpGet("{id}", Name = "Get")]
        [Route("{id:int}")]
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

        // GET: api/Book/Horror
        //[HttpGet("{genre}", Name = "Get")]
        [Route("{genre}")]
        public IEnumerable<Book> GetBooksByGenre(String genre)
        {
            try
            {
                string selectString = "select * from BOOK where genre LIKE @genre";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectString, conn))
                    {
                        command.Parameters.AddWithValue("@genre", genre);
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
            catch (Exception)
            {
                //future handling exceptions
                return null;
            }
        }

        // GET: api/Book/Horror
        //[HttpGet("{genre}", Name = "Get")]
        [Route("search/{name}")]
        public IEnumerable<Book> GetBooksByName(String name)
        {
            try
            {
                string selectString = "select * from dbo.BOOK as b where b.Title LIKE @title or b.Author LIKE @author";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectString, conn))
                    {
                        command.Parameters.AddWithValue("@title", name);
                        command.Parameters.AddWithValue("@author", name);
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


        // Book review's
        private BookReviews ReadBookReviews(SqlDataReader reader)
        {
            int reviewId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            int personId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            int bookId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            int rating = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
            string rText = reader.IsDBNull(4) ? "" : reader.GetString(4);
            string username = reader.IsDBNull(5) ? "" : reader.GetString(5);
            string userPhoto = reader.IsDBNull(6) ? "" : reader.GetString(6);

            BookReviews item = new BookReviews()
            {
                ReviewId = reviewId,
                PersonId = personId,
                BookId = bookId,
                ReviewRating = rating,
                ReviewText = rText,
                PersonUsername = username,
                PersonUserPhoto = userPhoto
            };

            return item;
        }

        // GET: api/Book/5/Reviews
        //[HttpGet("{bookId}", Name = "Get")]
        [Route("{bookId}/reviews")]
        public IEnumerable<BookReviews> GetReviews(int bookId)
        {
            try
            {
                string selectString = "select r.*, p.FullName, p.UserPhoto from dbo.REVIEW as r inner join dbo.PERSON as p on r.PersonId = p.PersonId where BookId = @id";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(selectString, conn))
                    {
                        command.Parameters.AddWithValue("@id", bookId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<BookReviews> result = new List<BookReviews>();
                            while (reader.Read())
                            {
                                BookReviews item = ReadBookReviews(reader);
                                result.Add(item);
                            }
                            return result;
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
    }
}
