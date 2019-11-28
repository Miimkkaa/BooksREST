using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int PersonId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
        public DateTime PurchaseDate { get; set; }

        

        public Order()
        {
            
        }

        public Order(int orderId, int personId, decimal totalPrice, bool paid, DateTime purchaseDate)
        {
            OrderId = orderId;
            PersonId = personId;
            TotalPrice = totalPrice;
            Paid = paid;
            PurchaseDate = purchaseDate;
        }

        //ToString
    }

}
