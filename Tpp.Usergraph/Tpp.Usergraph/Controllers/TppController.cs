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
            return Json(matchData, JsonRequestBehavior.AllowGet);
        }
    }
}
