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

                if (this.Session["errSeat"] != null)
                {
                    ViewBag.errSeat = this.Session["errSeat"];
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Commit(string[] Email, string[] First_name, string[] Middle_name, string[] Last_name, string[] Address, string[] Phone, string[] Gender, string[] City, string[] Province, string[] Postcode, string[] Class, int[] Price, string[] Seat, int Sum, int id_flight1, int id_flight2 = 0)
        {
            //generate ID Ticket auto-increment
            List<int> tickIDList = new List<int>();
            var tickIDQry = from t in db.Tickets
                              select t.ID;

            tickIDList.AddRange(tickIDQry.Distinct());
            int newID = 0;

            Debug.WriteLine("newID: "+newID);

            //dummy IDUser & IDFlight
            int IDUser = 1;

            //check no duplicate seat
            if (HasDuplicates(Seat))
            {
                //ViewBag.errSeat = "You must choose different seat";
                this.Session["errSeat"] = "You must choose different seat";
                return RedirectToAction("create", new FlightOption { id_flight1=id_flight1, id_flight2=id_flight2, Sum=Sum});
            }

            //insert to database
            for (int k = 0; k < Email.Length; k++)
            {
                if (ModelState.IsValid)
                {
                    Ticket newTicket = new Ticket(newID, IDUser, id_flight1, Email[k], First_name[k], Middle_name[k], Last_name[k], Address[k], Phone[k], Gender[k], City[k], Province[k], Postcode[k], Class[k], Price[k], Seat[k]);
                    db.Tickets.Add(newTicket);
                    db.SaveChanges();

                    if (id_flight2 != 0)
                    {
                        newTicket = new Ticket(newID, IDUser, id_flight2, Email[k], First_name[k], Middle_name[k], Last_name[k], Address[k], Phone[k], Gender[k], City[k], Province[k], Postcode[k], Class[k], Price[k], Seat[k]);
                        db.Tickets.Add(newTicket);
                        db.SaveChanges();
                    }
                }
            }
            //return View();
            return RedirectToAction("Index","Payment");
        }

        private bool HasDuplicates(string[] array)
        {
            List<string> vals = new List<string>();
            bool retval = false;
            foreach (string s in array)
            {
                if (vals.Contains(s))
                {
                    retval = true;
                    break;
                }
                vals.Add(s);
            }
            return retval;
        }

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