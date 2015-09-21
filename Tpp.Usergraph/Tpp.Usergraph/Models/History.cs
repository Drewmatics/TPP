using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tpp.Usergraph.Models
{
    public class History
    {
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<MatchData> MatchData { get; set; }
    }
}