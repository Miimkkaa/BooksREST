using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class PersonBook
    {
        public int PBId { get; set; }
        public int PersonId { get; set; }
        public int BookId { get; set; }

        public PersonBook()
        {

        }

        public PersonBook(int pBId, int personId, int bookId)
        {
            PBId = pBId;
            PersonId = personId;
            BookId = bookId;
        }
    }
}
