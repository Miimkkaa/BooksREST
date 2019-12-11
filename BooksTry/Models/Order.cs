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
        public string CardNumber { get; set; }
        public int ExpiryDate { get; set; }
        public int CVC { get; set; }
               
        public Order()
        {
            
        }

        public Order(int orderId, int personId, decimal totalPrice, bool paid, DateTime purchaseDate, string cardNumber, int expiryDate, int cVC)
        {
            OrderId = orderId;
            PersonId = personId;
            TotalPrice = totalPrice;
            Paid = paid;
            PurchaseDate = purchaseDate;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            CVC = cVC;
        }
    }

}
