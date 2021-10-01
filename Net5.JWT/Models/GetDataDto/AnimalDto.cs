
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Net5.JWT.Models.GetDataDto
{
    public class AnimalDto
    {
        [JsonRequired]
        [StringLength(15, MinimumLength = 5)]
        public string Title { get; set; }

        [JsonRequired]
        [StringLength(100, MinimumLength = 5)]
        public string Address { get; set; }

        [JsonRequired]
        [StringLength(8000, MinimumLength = 10)]
        public string Description { get; set; }

        [JsonRequired]
        public int UserId { get; set; }


        [JsonRequired]
        [StringLength(5000)]
        public string ImageUrl { get; set; }

        [JsonRequired]
        public int ShelterId { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> _extraStuff;
    }
}
