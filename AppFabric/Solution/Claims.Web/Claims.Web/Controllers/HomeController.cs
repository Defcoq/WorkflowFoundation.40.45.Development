using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claims.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Contoso Insurance Claims Processing Application!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
