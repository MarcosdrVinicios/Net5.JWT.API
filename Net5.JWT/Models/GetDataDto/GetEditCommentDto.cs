using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.GetDataDto
{
    public class GetEditCommentDto
    {
        [JsonRequired]
        [StringLength(500, MinimumLength = 5)]
        public string Comment { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> _extraStuff;
    }
}
