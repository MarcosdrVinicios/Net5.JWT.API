using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.SendDataDto
{
    public class SendAnimalDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string ImageUrl { get; set; }

        public int? ShelterId { get; set; }

        public SendAnimalDto(Found_Animal animal)
        {
            Id = animal.Id;
            Title = animal.Title;
            Address = animal.Address;
            Description = animal.Description;
            UserId = animal.UserRefId;
            ImageUrl = animal.ImagePath;
            ShelterId = animal.ShelterRefId;
        }
    }
}
