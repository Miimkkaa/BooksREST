using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksTry.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public int Type { get; set; } //in db is int, later on connect to enum
        public string UserPhoto { get; set; }

        public Person()
        {
                
        }

        public Person(int personId, string fullName, string username, string pass, string email, int type, string userPhoto)
        {
            PersonId = personId;
            FullName = fullName;
            Username = username;
            Pass = pass;
            Email = email;
            Type = type;
            UserPhoto = userPhoto;
        }
    }
}
