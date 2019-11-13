using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string BookDes { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int NoP{ get; set; }
        public int NoV { get; set; }

        // Later we should add the property for PDF

        public Book(int bookId, string title, string author, string bookDes, string genre, double price, int noP, int noV)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            BookDes = bookDes;
            Genre = genre;
            Price = price;
            NoP = noP;
            NoV = noV;
        }
        public Book()
        {

        }

        //ToString
    }
}
