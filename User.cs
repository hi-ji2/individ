using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRatingApp
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public User(int id, string username, string email, int roleId)
        {
            Id = id;
            Username = username;
            Email = email;
            RoleId = roleId;
        }
    }
}