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
        
        public BookOrder()
        {
            
        }

        public BookOrder(int bOId, int orderId, int bookid)
        {
            BOId = bOId;
            OrderId = orderId;
            Bookid = bookid;
        }
    }
}
