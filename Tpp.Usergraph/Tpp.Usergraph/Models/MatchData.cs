using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tpp.Usergraph.Models
{
    public class MatchData
    {
        public string Nickname { get; set; }
        public int Count { get; set; }
        [JsonProperty(PropertyName = "balance")]
        public List<BalanceData> Balances { get; set; }
        public List<KeyValuePair<int, int>> MaxPayouts { get; set; }
        public List<KeyValuePair<int, int>> MaxLosses { get; set; } 
    }
}