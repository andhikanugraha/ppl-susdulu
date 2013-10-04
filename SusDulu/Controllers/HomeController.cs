using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusDulu.Models;

namespace SusDulu.Controllers
{
    public class HomeController : Controller
    {
        DefaultConnection db = new DefaultConnection();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var airportList = new List<Airport>();
            var airportQuery = from a in db.Airports
                               select a;
            airportList.AddRange(airportQuery.Distinct());

            return View(airportList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
