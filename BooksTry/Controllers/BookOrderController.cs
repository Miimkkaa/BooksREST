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

            BookOrder item = new BookOrder()
            {
                BOId = boId,
                OrderId = orderId,
                Bookid = bookId,
            };

            return item;
        }

        // GET: api/Order/5
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

        // POST: api/BookOrder
        [HttpPost]
        public bool Post([FromBody] BookOrder value)
        {
            string inseartString = "INSERT INTO ORDERBOOK (OrdersId, BookId) values(@ordersId, @bookId); ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(inseartString, conn))
                {
                    command.Parameters.AddWithValue("@ordersId", value.OrderId);
                    command.Parameters.AddWithValue("@bookId", value.Bookid);

                    int rowsAffected = command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        // PUT: api/BookOrder/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{orderId}/{bookId}")]
        public int Delete(int orderId, int bookId)
        {
            string deleteString = "delete from ORDERBOOK where BookId = @bookId and OrdersId = @orderId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);
                    command.Parameters.AddWithValue("@orderId", orderId);
                    int rowAffected = command.ExecuteNonQuery();
                    return rowAffected;
                }
            }
        }
    }
}
