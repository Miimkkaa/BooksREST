using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class OrderedBooks
    {
        public int orderId { get; set; }
        public decimal totalPrice { get; set; }
        public int personId { get; set; }
        public int bookId { get; set; }
        public string bookTitle { get; set; }
        public string bookAuthor { get; set; }
        public decimal bookPrice { get; set; }
        public string bookCoverPhoto { get; set; }
            
        public OrderedBooks()
        {

        }

        public OrderedBooks(int orderId, decimal totalPrice, int personId, int bookId, string bookTitle, string bookAuthor, decimal bookPrice, string bookCoverPhoto)
        {
            this.orderId = orderId;
            this.totalPrice = totalPrice;
            this.personId = personId;
            this.bookId = bookId;
            this.bookTitle = bookTitle;
            this.bookAuthor = bookAuthor;
            this.bookPrice = bookPrice;
            this.bookCoverPhoto = bookCoverPhoto;
        }
    }
}
