using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BooksTry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            string userPhoto = reader.IsDBNull(6) ? "" : reader.GetString(6);

            Person item = new Person()
            {
                PersonId = id,
                FullName = fullName,
                Username = username,
                Pass = pass,
                Email = email,
                Type = type,
                UserPhoto = userPhoto
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
        public async void PostAsync([FromBody] Person value)
        {

            string insertString =
                "INSERT INTO PERSON (FullName, Username, Pass, Email, UserType) values(@FullName, @Username, @Pass, @Email, @type);";
            bool item = CheckUsernameValidation(value.Username);

            if (item == true)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(insertString, conn))
                    {

                        command.Parameters.AddWithValue("@FullName", value.FullName);
                        command.Parameters.AddWithValue("@Username", value.Username);
                        command.Parameters.AddWithValue("@Pass", value.Pass);
                        command.Parameters.AddWithValue("@Email", value.Email);
                        command.Parameters.AddWithValue("@type", 1);

                        int rowsAffected = command.ExecuteNonQuery();

                        await PostOrder(GetPersonId());

                        //return true;
                    }
                }
            }
            //else
                //return false;
        }

        //[Route("{usernameValidation}")]
        public bool CheckUsernameValidation(string usernameValidation)
        {
            string usernameValidationString = "SELECT * from PERSON WHERE username = @usernameV;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(usernameValidationString, conn))
                {
                    command.Parameters.AddWithValue("@usernameV", usernameValidation);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Person value)
        {
            string updateString = "UPDATE PERSON SET FullName=@FullName, Email=@Email, Pass=@Pass where PersonId = @id; ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(updateString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@FullName", value.FullName);
                    command.Parameters.AddWithValue("@Email", value.Email);
                    command.Parameters.AddWithValue("@Pass", value.Pass);
                    int rowAffected = command.ExecuteNonQuery();
                    return rowAffected;
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("login/{username}/{password}")]
        public Person Login(string username, string password)
        {
            var collection = Get();
            if (collection != null)
            {
                foreach (var person in collection)
                {
                    if ((person.Username == username) && (person.Pass == password))
                    {
                        return person;

                    }
                }
            }
            return null;
        }

        public int GetPersonId()
        {
            string selectString = "SELECT TOP 1 * FROM PERSON ORDER BY PersonId DESC";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return ReadItem(reader).PersonId;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        public async Task<bool> PostOrder(int personId)
        {
            string inseartString = "INSERT INTO ORDERS (PersonId, TotalPrice, Paid) values(@personId, @totalPrice, @paid); ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(inseartString, conn))
                {
                    command.Parameters.AddWithValue("@personId", personId);
                    command.Parameters.AddWithValue("@totalPrice", 0);
                    command.Parameters.AddWithValue("@paid", false);

                    int rowsAffected = command.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
