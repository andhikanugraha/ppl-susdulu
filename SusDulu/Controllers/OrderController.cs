using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusDulu.Models;
using System.Diagnostics;

namespace SusDulu.Controllers
{
    public class OrderController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // GET: /Order/Create

        public ActionResult Create(FlightOption flight = null)
        {
            if (flight != null)
            {
                Debug.WriteLine("SUM: " + flight.Sum);
                Debug.WriteLine("FLIGHT1: " + flight.id_flight1);
                Debug.WriteLine("FLIGHT2: " + flight.id_flight2);

                //daftar seat yang telah dipesan
                List<string> occSeat = new List<string>();

                var seatQry = from s in db.Tickets
                              select s.Seat;
                occSeat.AddRange(seatQry.Distinct());
                ViewBag.occSeat = occSeat;

                //dummy susunan seat terkait model pesawat
                ViewBag.seatCol = "ABCDEF";
                ViewBag.seatRow = 6;

                //pesan terkait tiket
                ViewBag.Sum = flight.Sum;
                ViewBag.IDFlight1 = flight.id_flight1;
                ViewBag.IDFlight2 = flight.id_flight2;
            }

            return View();
        }

        public ActionResult Commit(int id_flight1, int id_flight2, string Email, string First_name, string Middle_name, string Last_name, string Address, string Phone, string Gender, string City, string Province, string Postcode, string Class, int Price, string Seat, int Sum)
        {
            Debug.WriteLine("EMAIL: " + Email);
            Debug.WriteLine("FIRST_NAME: " + First_name);
            Debug.WriteLine("PRICE: " + Price);

            //generate ID Ticket auto-increment
            List<int> tickIDList = new List<int>();
            var tickIDQry = from t in db.Tickets
                              select t.ID;

            tickIDList.AddRange(tickIDQry.Distinct());
            int newID = tickIDList.Last() + 1;

            Debug.WriteLine("NEW_ID: "+newID);

            //dummy IDUser & IDFlight
            int IDFlight = 1;
            int IDUser = 1;

            //insert to database
            if (ModelState.IsValid)
            {
                Ticket newTicket = new Ticket(newID,IDUser,IDFlight,Email,First_name,Middle_name,Last_name,Address,Phone,Gender,City,Province,Postcode,Class,Price,Seat);
                db.Tickets.Add(newTicket);
                db.SaveChanges();
            }

            if (Sum == 1)
            {
                return View();
            }
            else
            {
                //Redirect ke halaman create lagi
                Debug.WriteLine("SUM TIDAK NOL");
                return RedirectToAction("Create", "Order", new FlightOption { id_flight1 = id_flight1, id_flight2 = id_flight2, Sum = Sum-1 });
            }
            //return View();
        }

        //
        // POST: /Order/Create

        //[HttpPost]
        //public ActionResult Create(Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tickets.Add(ticket);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(ticket);
        //}

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Order/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}