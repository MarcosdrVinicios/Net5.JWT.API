using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models
{
    public class User_Role
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Role { get; set; }

        public User_Role() { }

        public User_Role(User_Role role)
        {
            Id = role.Id;
            Role = role.Role;
        }
    }
}
