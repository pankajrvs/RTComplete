using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RT.Services.Controllers
{
    public class RTController : Controller
    {
        // GET: RT
        public ActionResult Index()
        {
            return View();
        }
    }
}