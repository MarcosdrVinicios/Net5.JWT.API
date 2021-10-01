using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; set; }

        [JsonRequired]
        [Required]
        [StringLength(80)]
        public string ShelterName { get; set; }

        [JsonRequired]
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [JsonRequired]
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [JsonRequired]
        [Required]
        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [JsonRequired]
        [Required]
        [StringLength(8000)]
        public string Description { get; set; }

        [JsonRequired]
        [Required]
        [StringLength(8000)]
        public string ShelterImage { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> _extraStuff;

        [JsonIgnore]
        public ICollection<Found_Animal> Found_Animals { get; set; }
    }
}
