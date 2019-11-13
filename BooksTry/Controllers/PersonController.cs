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
    public class PersonController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            string selectString = "select * from PERSON;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Person> result = new List<Person>();
                        while (reader.Read())
                        {
                            Person item = ReadItem(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        private Person ReadItem(SqlDataReader reader)
        {
            int id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            string fullName = reader.IsDBNull(1) ? "" : reader.GetString(1);
            string username = reader.IsDBNull(2) ? "" : reader.GetString(2);
            string pass = reader.IsDBNull(3) ? "" : reader.GetString(3);
            string email = reader.IsDBNull(4) ? "" : reader.GetString(4);
            int type = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);

            Person item = new Person()
            {
                PersonId = id,
                FullName = fullName,
                Username = username,
                Pass = pass,
                Email = email,
                Type = type
            };

            return item;
        }

        // GET: api/Person/5
        [Route("{id}")]
        public Person Get(int id)
        {
            try
            {
                string selectString = "select * from PERSON where PersonId = @id";
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

        // POST: api/Person
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Person/5
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
