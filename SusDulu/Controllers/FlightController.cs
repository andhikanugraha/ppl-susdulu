using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusDulu.Models;
using System.Diagnostics;
using System.Globalization;
using System.Data.Objects;

namespace SusDulu.Controllers
{
    public class FlightController : Controller
    {
        private FlightDBContext db = new FlightDBContext();

        //
        // GET: /Flight/

        public ActionResult Index()
        {
            return View(db.Flights.ToList());
        }

        //
        // GET: /Flight/Details/5

        public ActionResult Details(int id = 0)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        //
        // GET: /Flight/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Flight/Create

        [HttpPost]
        public ActionResult Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        //
        // GET: /Flight/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        //
        // POST: /Flight/Edit/5

        [HttpPost]
        public ActionResult Edit(Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        //
        // GET: /Flight/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        //
        // POST: /Flight/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SearchIndex(string flightOrigin, string flightDestination, string flightSchedule)
        {
            Debug.WriteLine("pesan tiket clicked");
            Debug.WriteLine("schedule: "+flightSchedule);
            DateTime dt = new DateTime();
            if (!string.IsNullOrEmpty(flightSchedule))
            {
                dt = DateTime.ParseExact(flightSchedule, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Debug.WriteLine("schedule baru: " + dt.ToShortDateString());
                flightSchedule = dt.ToShortDateString();
            }
            

            var OriginList = new List<string>();
            var DestinationList = new List<string>();
            var ScheduleList = new List<string>();

            //            buat select list
            var OriginQry = from o in db.Flights
                            orderby o.origin
                            select o.origin;
            OriginList.AddRange(OriginQry.Distinct());
            ViewBag.flightOrigin = new SelectList(OriginList);

            var DestinationQry = from d in db.Flights
                                 orderby d.destination
                                 select d.destination;
            DestinationList.AddRange(DestinationQry.Distinct());
            ViewBag.flightDestination = new SelectList(DestinationList);

            var ScheduleQry = from d in db.Flights
                              orderby d.schedule
                              select d.schedule;

            //            mekanisme seleksi
            var flights = from f in db.Flights
                          select f;

            foreach (var dum in flights)
            {
                Debug.WriteLine("sch: "+dum.schedule.ToShortDateString());
            }

            if (!String.IsNullOrEmpty(flightOrigin))
            {
                flights = flights.Where(s => s.origin.Equals(flightOrigin));
            }

            if (string.IsNullOrEmpty(flightDestination))
            {
                //return View(flights);
            }
            else
            {
                //return View(flights.Where(x => x.destination == flightDestination));
                flights = flights.Where(x => x.destination == flightDestination);
            }

            if (string.IsNullOrEmpty(flightSchedule))
            {
                return View(flights);
            }
            else
            {
                Debug.WriteLine("masuk seleksi schedule");
                return View(flights.Where(x => EntityFunctions.TruncateTime(x.schedule) == dt));
            }
        }
    }
}