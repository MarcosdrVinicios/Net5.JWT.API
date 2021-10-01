using System.ComponentModel.DataAnnotations;

namespace Net5.JWT.Models.GetDataDto
{
    public class GetSheltersDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string ShelterName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public GetSheltersDto(Shelter shelter)
        {
            Id = shelter.Id;
            ShelterName = shelter.ShelterName;
            Address = shelter.Address;
            Email = shelter.Email;
        }
    }
}
