using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.GetDataDto
{
    public class UserListDto
    {
            public int Id { get; set; }

            public string Username { get; set; }
            public string Email { get; set; }

            public int Role { get; set; }

        public UserListDto(User u)
        {
            Id = u.Id;
            Username = u.Username;
            Email = u.Email;
            Role = u.RoleRefId;
        }
    }
}
