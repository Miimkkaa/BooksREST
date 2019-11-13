using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int PersonId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string RText { get; set; }

        public Review()
        {

        }

        public Review(int reviewId, int personId, int bookId, int rating, string rText)
        {
            ReviewId = reviewId;
            PersonId = personId;
            BookId = bookId;
            Rating = rating;
            RText = rText;
        }
    }
}
