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

        public ActionResult Index(string message = null)
        {
            ViewBag.Message = message;
            var airportList = new List<Airport>();
            var airportQuery = from airport in db.Airports
                               select airport;
            airportList.AddRange(airportQuery.Distinct());

            return View(airportList);
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new {message = "search_error"});
            }
            if (model.Sum < 1)
            {
                return RedirectToAction("Index", new {message = "input_error"});
            }
            model.Departure_date1.Replace("/", "-");
            var flightList1 = new List<Flight>();
            var flightQuery = from flight in db.Flights
                              select flight;
            flightQuery = flightQuery.Where(f => f.Origin.Equals(model.Origin));
            flightQuery = flightQuery.Where(f => f.Destination.Equals(model.Destination));
            flightQuery = flightQuery.Where(f => f.Departure_date.Equals(model.Departure_date1));
            flightList1.AddRange(flightQuery.Distinct());

            var flightList2 = new List<Flight>();
            if (model.TripType.Equals("round-trip"))
            {
                flightQuery = from flight in db.Flights
                              select flight;
                flightQuery = flightQuery.Where(f => f.Origin.Equals(model.Destination));
                flightQuery = flightQuery.Where(f => f.Destination.Equals(model.Origin));
                flightQuery = flightQuery.Where(f => f.Departure_date.Equals(model.Departure_date2));
                flightList2.AddRange(flightQuery.Distinct());
            }

            Session["jmlTiket"] = model.Sum;

            return View(new OptionsModel() { TripType = model.TripType, Flights1 = flightList1, Flights2 = flightList2, Sum = model.Sum });
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
