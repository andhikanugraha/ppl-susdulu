﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusDulu.Models;

namespace SusDulu.Controllers
{
    public class FlightController : Controller
    {
        private FlightDBContext flight_db = new FlightDBContext();
        private SusDuluDbContext db = new SusDuluDbContext();

        //
        // GET: /Flight/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Flight/Details/5

        public ActionResult Details(int id = 0)
        {
            Flight flight = flight_db.Flights.Find(id);
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
                flight_db.Flights.Add(flight);
                flight_db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        //
        // GET: /Flight/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Flight flight = flight_db.Flights.Find(id);
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
                flight_db.Entry(flight).State = EntityState.Modified;
                flight_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        //
        // GET: /Flight/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Flight flight = flight_db.Flights.Find(id);
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
            Flight flight = flight_db.Flights.Find(id);
            flight_db.Flights.Remove(flight);
            flight_db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Flight/Search/

        public ActionResult Search()
        {
            return View();
        }

        //
        // POST: /Flight/List/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(Flight flight)
        {
            var flightList = new List<Flight>();
            var flightQuery = from f in db.flight
                              select f;
            if (flight.origin != null)
            {
                flightQuery = flightQuery.Where(f => f.origin.Equals(flight.origin));
            }
            if (flight.destination != null)
            {
                flightQuery = flightQuery.Where(f => f.destination.Equals(flight.destination));
            }

            if (flight.schedule != null)
            {
                flightQuery = flightQuery.Where(f => f.schedule.Equals(flight.schedule));
            }
            flightList.AddRange(flightQuery);

            return View(flightList);
        }

        //
        // GET: /Flight/SeacrhTicket

        public ActionResult SearchTicket()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            flight_db.Dispose();
            base.Dispose(disposing);
        }

        //
        // POST: /Flight/PrintTicket

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintTicket(string id_ticket)
        {
            var ticketList = new List<Ticket>();
            var flightList = new List<Flight>();
            var forList = new List<For>();

            var ticketQuery = from ticket in db.ticket
                              select ticket;
            ticketQuery = ticketQuery.Where(t => t.Id_ticket.Equals(id_ticket));
            ticketList.AddRange(ticketQuery);

            if (ticketList.Count > 0)
            {
                var forQuery = from _for in db.For
                               select _for;
                forQuery = forQuery.Where(f => f.Id_ticket.Equals(id_ticket));
                forList.AddRange(forQuery);
                string idFlight = forList[0].Id_flight;

                var flightQuery = from flight in db.flight
                                  select flight;
                flightQuery = flightQuery.Where(f => f.Id_flight.Equals(idFlight));
                flightList.AddRange(flightQuery);

                return View(new FlightTicket() { flight = flightList[0], ticket = ticketList[0]});
            }
            else 
            {
                return HttpNotFound();
            }
        }

        public ActionResult SearchIndex(string flightOrigin, string flightDestination)
        {
            var OriginList = new List<string>();
            var DestinationList = new List<string>();

            //            buat select list
            var OriginQry = from o in flight_db.Flights
                            orderby o.origin
                            select o.origin;
            OriginList.AddRange(OriginQry.Distinct());
            ViewBag.flightOrigin = new SelectList(OriginList);

            var DestinationQry = from d in flight_db.Flights
                                 orderby d.destination
                                 select d.destination;
            DestinationList.AddRange(DestinationQry.Distinct());
            ViewBag.flightDestination = new SelectList(DestinationList);

            //            mekanisme seleksi
            var flights = from f in flight_db.Flights
                          select f;

            if (!String.IsNullOrEmpty(flightOrigin))
            {
                flights = flights.Where(s => s.origin.Equals(flightOrigin));
            }

            if (string.IsNullOrEmpty(flightDestination))
            {
                return View(flights);
            }
            else
            {
                return View(flights.Where(x => x.destination == flightDestination));
            }
        }
    }
}