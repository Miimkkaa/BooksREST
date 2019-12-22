using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class Bookshelf
    {
        public int PersonId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string CoverPhoto { get; set; }

        public Bookshelf(int personId, int bookId, string bookTitle, string bookAuthor, string coverPhoto)
        {
            PersonId = personId;
            BookId = bookId;
            BookTitle = bookTitle;
            BookAuthor = bookAuthor;
            CoverPhoto = coverPhoto;
        }

        public Bookshelf()
        {

        }
    }
}
