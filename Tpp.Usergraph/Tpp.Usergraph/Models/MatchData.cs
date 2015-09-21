using System.Collections.Generic;

namespace Tpp.Usergraph.Models
{
    public class MatchData
    {
        public string Nickname { get; set; }
        public int Count { get; set; }
        public List<Balance> Balance { get; set; }
    }
}