using Newtonsoft.Json;
using System;

namespace Tpp.Usergraph.Models
{
    public class BalanceData
    {
        [JsonProperty(PropertyName = "battle_id")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Balance { get; set; }
        public bool Won { get; set; }
        [JsonProperty(PropertyName = "won_amount")]
        public int Payout { get; set; }
    }
}