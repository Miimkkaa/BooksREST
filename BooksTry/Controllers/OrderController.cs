﻿using System;
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
    public class OrderController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/Order
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            string selectString = "select * from ORDERS;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Order> result = new List<Order>();
                        while (reader.Read())
                        {
                            Order item = ReadItem(reader);
                            result.Add(item);
                        }
                        return result;
                    }
                }
            }
        }

        private Order ReadItem(SqlDataReader reader)
        {
            int ordersId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            int personId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            decimal totalPrice = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
            bool paid = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
            DateTime purchaseDate = reader.IsDBNull(4) ? DateTime.Parse("1900-11-11T00:00:00.00") : reader.GetDateTime(4);
            string cardNumber = reader.IsDBNull(5) ? "" : reader.GetString(5);
            int expiryDate = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
            int cvc = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);

            Order item = new Order()
            {
                OrderId = ordersId,
                PersonId = personId,
                TotalPrice = totalPrice,
                Paid = paid,
                PurchaseDate = purchaseDate,
                CardNumber = cardNumber,
                ExpiryDate = expiryDate,
                CVC = cvc
            };

            return item;
        }

        // GET: api/Order/5
        //[HttpGet("{id}", Name = "Get")]
        [Route("{id}")]
        public Order Get(int id)
        {
            try
            {
                string selectString = "select * from ORDERS where OrdersId = @id";
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

        // POST: api/Order
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //update order: for payment
        // PUT: api/Order/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Order value)
        {
            string updateString = "update ORDERS set Paid = @paid, PurchaseDate = @purchaseDate," +
                " CardNumber = @cardNumber, ExpiryDate = @ExpiryDate, CVC = @cvc where OrdersId = @id; ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(updateString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@paid", true);
                    command.Parameters.AddWithValue("@purchaseDate", DateTime.Now);
                    command.Parameters.AddWithValue("@cardNumber", value.CardNumber);
                    command.Parameters.AddWithValue("@ExpiryDate", value.ExpiryDate);
                    command.Parameters.AddWithValue("@cvc", value.CVC);
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
    }
}
