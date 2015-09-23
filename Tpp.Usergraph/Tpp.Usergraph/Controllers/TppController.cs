using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Tpp.Usergraph.Models;

namespace Tpp.Usergraph.Controllers
{
    public class TppController : Controller
    {
        private const string BaseUrl = "http://www.twitchplaysleaderboard.info/api";
        public ActionResult Index()
        {
            ViewBag.Message = "Use this to see a time vs. balance graph of your TPP earnings!";
            return View();
        }

        [HttpPost]
        public ActionResult Graph(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        public ActionResult History(string username)
        {
            var restClient = new RestClient(BaseUrl);
            var queryParams = new List<string> { "unverified=true" };
            var restRequest = new RestRequest(string.Format("/user/history/{0}?{1}", username, string.Join(",", queryParams)));

            var response = restClient.Get(restRequest);
            var serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var userHistory = JsonConvert.DeserializeObject<History>(response.Content, serializerSettings);
            var matchData = userHistory.MatchData.FirstOrDefault();
            if (matchData == null)
                return null;
            matchData.MaxPayouts= GetBiggestPayouts(matchData, 5);
            matchData.MaxLosses = GetBiggestLosses(matchData, 5);
            matchData.Balances.RemoveAll(x => x.Balance <= 0);
            return Json(matchData, JsonRequestBehavior.AllowGet);
        }

        private static List<KeyValuePair<int, int>> GetBiggestPayouts(MatchData matchData, int numOfPayouts)
        {
            return GetPayouts(matchData).OrderByDescending(x => x.Value).Take(numOfPayouts).ToList();
        }

        private static List<KeyValuePair<int, int>> GetBiggestLosses(MatchData matchData, int numOfPayouts)
        {
            return GetPayouts(matchData).OrderBy(x => x.Value).Take(numOfPayouts).ToList();
        } 

        private static Dictionary<int, int> GetPayouts(MatchData matchData)
        {
            var balances = matchData.Balances.Where(x => x.Id > 19500).ToList();
            var payouts = new Dictionary<int, int>();
            var firstBalance = balances.First();
            balances.Remove(firstBalance);
            foreach (var balance in balances)
            {
                var amount = balance.Balance - firstBalance.Balance;
                if (firstBalance.Balance == 0)
                    amount = 0;
                payouts.Add(firstBalance.Id, amount);
                firstBalance = balance;
            }
            return payouts;
        }
    }
}
