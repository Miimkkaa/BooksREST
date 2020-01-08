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
    public class ReviewController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/Review
        [HttpGet]
        public IEnumerable<Review> Get()
        {
            string selectString = "select * from REVIEW;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Review> result = new List<Review>();
                        while (reader.Read())
                        {
                            Review item = ReadItem(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        private Review ReadItem(SqlDataReader reader)
        {
            int reviewId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            int personId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            int bookId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            int rating = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
            string rText = reader.IsDBNull(4) ? "" : reader.GetString(4);

            Review item = new Review()
            {
                ReviewId = reviewId,
                PersonId = personId,
                BookId = bookId,
                Rating = rating,
                RText = rText
            };

            return item;
        }

        // GET: api/Review/5
        [Route("{id}")]
        public Review Get(int id)
        {
            try
            {
                string selectString = "select * from REVIEW where ReviewId = @id";
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

        // POST: api/Review
        [HttpPost]
        public bool Post([FromBody] Review value)
        {
            string insertString = "insert into REVIEW (PersonId, BookId, RText) values(@personId, @bookId, @rText);";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                    command.Parameters.AddWithValue("@personId", value.PersonId);
                    command.Parameters.AddWithValue("@bookId", value.BookId);
                    command.Parameters.AddWithValue("@rText", value.RText);

                    int rowsAffected = command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        // PUT: api/Review/5
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
