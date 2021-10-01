using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.GetDataDto
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("Found_Animal")]
        public int Found_AnimalRefId { get; set; }

        [JsonIgnore]
        public Found_Animal Found_Animal { get; set; }
    }
}
