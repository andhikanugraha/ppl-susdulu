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
        public ActionResult Proceed(string[] Email, string[] First_name, string[] Middle_name, string[] Last_name, string[] Address, string[] Phone, string[] Gender, string[] City, string[] Province, string[] Postcode, string[] Class, int[] Price, string[] Seat, int Sum, int id_flight1, int id_flight2 = 0)
        {
            List<string> emailL = new List<string>();
            List<string> firstL = new List<string>();
            List<string> middleL = new List<string>();
            List<string> lastL = new List<string>();
            List<string> addressL = new List<string>();
            List<string> phoneL = new List<string>();
            List<string> genderL = new List<string>();
            List<string> cityL = new List<string>();
            List<string> provinceL = new List<string>();
            List<string> postcodeL = new List<string>();
            List<string> classL = new List<string>();
            List<int> priceL = new List<int>();
            List<string> seatL = new List<string>();

            var passengers = new List<FauxTicket>();

            for (int k = 0; k < Email.Length; k++)
            {
                emailL.Add(Email[k]);
                firstL.Add(First_name[k]);
                middleL.Add(Middle_name[k]);
                lastL.Add(Last_name[k]);
                addressL.Add(Address[k]);
                phoneL.Add(Phone[k]);
                genderL.Add(Gender[k]);
                cityL.Add(City[k]);
                provinceL.Add(Province[k]);
                postcodeL.Add(Postcode[k]);
                classL.Add(Class[k]);
                priceL.Add(Price[k]);
                seatL.Add(Seat[k]);

                string fullName = First_name[k];
                if (Middle_name[k].Length > 0)
                    fullName += " " + Middle_name[k];
                if (Last_name[k].Length > 0)
                    fullName += " " + Last_name[k];

                var passenger = new FauxTicket()
                {
                    Name = fullName,
                    SeatClass = Class[k],
                    Price = Price[k],
                    Seat = Seat[k]
                };

                passengers.Add(passenger);
            }

            //Duplication Check
            if (HasDuplicates(seatL))
            {
                TempData["errSeat"] = "Anda harus memilih tempat duduk yang berbeda";
                return RedirectToAction("create", new FlightOption { id_flight1 = id_flight1, id_flight2 = id_flight2, Sum = Sum });
            }

            TempData["email"] = emailL;
            TempData["first"] = firstL;
            TempData["middle"]= middleL;
            TempData["last"] = lastL;
            TempData["address"] = addressL;
            TempData["phone"] = phoneL;
            TempData["gender"] = genderL;
            TempData["city"] = cityL;
            TempData["province"] = provinceL;
            TempData["postcode"] = postcodeL;
            TempData["class"] = classL;
            TempData["price"] = priceL;
            TempData["seat"] = seatL;
            TempData["idFlight1"] = id_flight1;
            TempData["idFlight2"] = id_flight2;
            TempData["sum"] = Sum;

            ViewBag.flight1 = db.Flights.Find(id_flight1);
            if (id_flight2 != 0)
            {
                ViewBag.isRoundTrip = true;
                ViewBag.flight2 = db.Flights.Find(id_flight2);
            }
            else
            {
                ViewBag.isRoundTrip = false;
            }

            ViewBag.origin = ViewBag.flight1.GetOriginAirport();
            ViewBag.destination = ViewBag.flight1.GetDestinationAirport();

            ViewBag.Sum = Sum;
            ViewBag.Passengers = passengers;

            return View();
        }

        public ActionResult PaymentGateway(int total, string origin, string destination, int pax)
        {
            string paymentGateway = System.Configuration.ConfigurationManager.AppSettings.Get("paymentGateway");
            string gatewayTemplate = paymentGateway + "/pay/erlangga?total={0}&origin={1}&destination={2}&pax={3}&handler={4}";
            string handlerUrl = Url.Action("Index", "Payment", null, Request.Url.Scheme);

            string to = string.Format(gatewayTemplate, total, origin, destination, pax, handlerUrl);

            return Redirect(to);
        }

        private bool HasDuplicates(List<string> array)
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