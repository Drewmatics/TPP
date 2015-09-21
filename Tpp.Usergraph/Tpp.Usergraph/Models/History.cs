using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tpp.Usergraph.Models
{
    public class History
    {
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<MatchData> MatchData { get; set; }
    }
}