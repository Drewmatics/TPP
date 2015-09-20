using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Tpp.Usergraph.Models;

namespace Tpp.Usergraph.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Use this to see a time vs. balance graph of your TPP earnings!";

            return View();
        }

        [HttpPost]
        public void Graph(string username)
        {
            var baseUrl = string.Format("http://www.twitchplaysleaderboard.info/api");
            var restClient = new RestClient(baseUrl);
            var restRequest = new RestRequest(string.Format("/user/history/{0}", username));
            
            var response = restClient.Get(restRequest);
            var userHistory = JsonConvert.DeserializeObject<History>(response.Content);
            // TODO: Do something with response
        }
    }
}
