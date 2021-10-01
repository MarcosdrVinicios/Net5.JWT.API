using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Net5.JWT.Models
{
    public class User
    {
        [Key]
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Newtonsoft.Json.JsonProperty("username")]
        [StringLength(255)]
        public string Username { get; set; }

        [JsonIgnore]
        [Required]
        [Newtonsoft.Json.JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [System.Text.Json.Serialization.JsonIgnore]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [Newtonsoft.Json.JsonProperty("role")]
        [ForeignKey("User_Role")]
        public int RoleRefId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonProperty("email")]
        public User_Role User_Role { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Found_Animal> Found_Animals { get; set; }
    }
}
