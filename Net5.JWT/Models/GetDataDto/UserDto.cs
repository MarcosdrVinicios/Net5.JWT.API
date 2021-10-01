using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.GetDataDto
{
    public class UserDto
    {   
        [JsonRequired]
        public string Email { get; set; }

        [JsonRequired]
        [StringLength(255)]
        public string Password { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> _extraStuff;
    }
}
