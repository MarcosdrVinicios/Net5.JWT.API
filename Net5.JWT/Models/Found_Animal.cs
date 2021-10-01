using Net5.JWT.Models.GetDataDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models
{
    public class Found_Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(8000)]
        public  string Description { get; set; }

        [StringLength(5000)]
        public string ImagePath { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserRefId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Shelter")]
        public int? ShelterRefId { get; set; }
        public Shelter Shelter { get; set; }

       // [CascadeDelete]
        public ICollection<Comments> Comments { get; set; }
    }
}
