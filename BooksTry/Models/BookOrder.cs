using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class BookOrder
    {
        public int BOId { get; set; }
        public int OrderId { get; set; }
        public int Bookid { get; set; }
        public int CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CVV { get; set; }

        public BookOrder(int boId, int orderId, int bookid, int cardNumber, DateTime expiryDate, int cvv)
        {
            BOId = boId;
            OrderId = orderId;
            Bookid = bookid;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            CVV = cvv;
        }

        public BookOrder()
        {
            
        }

        //ToString
    }
}
