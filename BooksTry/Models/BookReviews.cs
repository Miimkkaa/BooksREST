using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class BookReviews
    {
        public int ReviewId { get; set; }
        public int PersonId { get; set; }
        public int BookId { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewText { get; set; }
        public string PersonUsername { get; set; }
        public string PersonUserPhoto { get; set; }

        public BookReviews()
        {

        }

        public BookReviews(int reviewId, int personId, int bookId, int reviewRating, string reviewText, string personUsername, string personUserPhoto)
        {
            ReviewId = reviewId;
            PersonId = personId;
            BookId = bookId;
            ReviewRating = reviewRating;
            ReviewText = reviewText;
            PersonUsername = personUsername;
            PersonUserPhoto = personUserPhoto;
        }

    }
}
