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
        public bool Post([FromBody] Person value)
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
                        return true;
                    }
                }
            }
            else
                return false;
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

        // Update user's full name and email
        // PUT: api/Person/5 
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Person value)
        {
            string updateString = "UPDATE PERSON SET FullName=@FullName, Email=@Email where PersonId = @id; ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(updateString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@FullName", value.FullName);
                    command.Parameters.AddWithValue("@Email", value.Email);
                    int rowAffected = command.ExecuteNonQuery();
                    return rowAffected;
                }
            }
        }

        // Update user's password
        // PUT: api/Person/5
        [HttpPut("passChange/{id}")]
        public int PutPass(int id, [FromBody] Person value)
        {
            string updateString = "UPDATE PERSON SET Pass=@Pass where PersonId = @id;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(updateString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@Pass", value.Pass);
                    int rowAffected = command.ExecuteNonQuery();
                    return rowAffected;
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("firstDel/{personId}")]
        public bool Delete(int personId)
        {
            string deleteString = "DELETE FROM PERSONBOOK WHERE PersonId=@PersonId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    int rowAffected = command.ExecuteNonQuery();
                    return true;
                }
            }
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("secondDel/{personId}")]
        public int DeletePerson(int personId)
        {
            string deleteString = "DELETE FROM PERSON WHERE PersonId=@PersonId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    int rowAffected = command.ExecuteNonQuery();
                    return rowAffected;
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("delAccount/{personId}")]
        public int DeleteAccount(int personId)
        {
            string deleteString =
                "BEGIN TRANSACTION SET XACT_ABORT ON DELETE PERSONBOOK WHERE PersonId=@PersonId DELETE PERSON WHERE PersonId=@PersonId COMMIT TRANSACTION";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    int rowAffected = command.ExecuteNonQuery();
                    return rowAffected;
                }
            }
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
    }
}
