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
    public class PersonBookController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/PersonBook
        [HttpGet]
        public IEnumerable<PersonBook> Get()
        {
            string selectString = "select * from PERSONBOOK;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<PersonBook> result = new List<PersonBook>();
                        while (reader.Read())
                        {
                            PersonBook item = ReadItem(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        private PersonBook ReadItem(SqlDataReader reader)
        {
            int pBId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            int personId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            int bookId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);

            PersonBook item = new PersonBook()
            {
                PBId = pBId,
                PersonId = personId,
                BookId = bookId
            };

            return item;
        }

        // GET: api/PersonBook/5
        [Route("{id}")]
        public PersonBook Get(int id)
        {
            try
            {
                string selectString = "select * from PERSONBOOK where PBId = @id";
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

        // POST: api/PersonBook
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PersonBook/5
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
