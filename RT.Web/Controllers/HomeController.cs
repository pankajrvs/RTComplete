using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getPlayer(string playerName)
        {
            return View();
        }
        public ActionResult OnlinePlayers(string clanName)
        {
            return View();
        }
    }
}