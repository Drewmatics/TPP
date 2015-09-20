using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tpp.Usergraph.Models
{
    public class Balance
    {
        [JsonProperty(PropertyName = "battle_id")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "balance")]
        public int Amount { get; set; }
        public bool Won { get; set; }
    }
}