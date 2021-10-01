using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.GetDataDto
{
    public class GetShelterSelectDto
    {
        public int Id { get; set; }
        public string ShelterName { get; set; }

        public GetShelterSelectDto(int id, string name)
        {
            Id = id;
            ShelterName = name;
        }
    }
}
