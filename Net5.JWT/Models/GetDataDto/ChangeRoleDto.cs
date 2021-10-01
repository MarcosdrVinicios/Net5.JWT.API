using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models.GetDataDto
{
    public class ChangeRoleDto
    {
        [JsonRequired]
        public int Role { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> _extraStuff;
    }
}
