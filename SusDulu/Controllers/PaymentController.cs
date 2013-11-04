using SusDulu.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using RazorPDF;

namespace SusDulu.Controllers
{
    public class PaymentController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        //
        // GET: /Payment/

        public ActionResult Index()
        {
            if (TempData["idFlight1"] == null)
            {
                ViewBag.Error = true;
            }
            else
            {
                ViewBag.Error = false;
                List<string> emailL = (List<string>)TempData["email"];
                List<string> firstL = (List<string>)TempData["first"];
                List<string> middleL = (List<string>)TempData["middle"];
                List<string> lastL = (List<string>)TempData["last"];
                List<string> addressL = (List<string>)TempData["address"];
                List<string> phoneL = (List<string>)TempData["phone"];
                List<string> genderL = (List<string>)TempData["gender"];
                List<string> cityL = (List<string>)TempData["city"];
                List<string> provinceL = (List<string>)TempData["province"];
                List<string> postcodeL = (List<string>)TempData["postcode"];
                List<string> classL = (List<string>)TempData["class"];
                List<int> priceL = (List<int>)TempData["price"];
                List<string> seatL = (List<string>)TempData["seat"];

                int id_flight1 = int.Parse(TempData["idFlight1"].ToString());
                int id_flight2 = int.Parse(TempData["idFlight2"].ToString());
                int sum = int.Parse(TempData["sum"].ToString());
                if (id_flight2 != 0)
                {
                    sum *= 2;
                }

                //generate ID Ticket auto-increment
                List<int> tickIDList = new List<int>();
                var tickIDQry = from t in db.Tickets
                                select t.ID;
                tickIDList.AddRange(tickIDQry.Distinct());
                int lastElmt;
                try
                {
                    lastElmt = tickIDList.Last();
                }
                catch (InvalidOperationException e)
                {
                    lastElmt = 0;
                }
                int newID = lastElmt + 1;

                int? IDUser = WebSecurity.CurrentUserId;
                if (IDUser < 0) IDUser = null;

                Debug.WriteLine("IDUSER: " + IDUser);

                var tickets = new List<Ticket>();

                for (int k = 0; k < emailL.Count; k++)
                {
                    if (ModelState.IsValid)
                    {
                        //get certain IDUser
                        List<int> userIDList = new List<int>();

                        Ticket departureTicket = new Ticket()
                        {
                            ID_user = IDUser,
                            ID_flight = id_flight1,
                            Email = emailL[k],
                            First_name = firstL[k],
                            Middle_name = middleL[k],
                            Last_name = lastL[k],
                            Address = addressL[k],
                            Phone = phoneL[k],
                            Gender = genderL[k],
                            City = cityL[k],
                            Province = provinceL[k],
                            Postcode = postcodeL[k],
                            Class = classL[k],
                            Price = priceL[k],
                            Seat = seatL[k]
                        };

                        db.Tickets.Add(departureTicket);
                        tickets.Add(departureTicket);

                        if (id_flight2 != 0)
                        {
                            Ticket returnTicket = new Ticket()
                            {
                                ID_user = IDUser,
                                ID_flight = id_flight2,
                                Email = emailL[k],
                                First_name = firstL[k],
                                Middle_name = middleL[k],
                                Last_name = lastL[k],
                                Address = addressL[k],
                                Phone = phoneL[k],
                                Gender = genderL[k],
                                City = cityL[k],
                                Province = provinceL[k],
                                Postcode = postcodeL[k],
                                Class = classL[k],
                                Price = priceL[k],
                                Seat = seatL[k]
                            };

                            db.Tickets.Add(returnTicket);
                            tickets.Add(returnTicket);
                        }
                    }
                }

                db.SaveChanges();

                ViewBag.sum = sum;
                ViewBag.firstID = newID;
                ViewBag.tickets = tickets;
            }
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Print(int idTiket)
        {
            Ticket tiket = db.Tickets.Find(idTiket);
            if (tiket == null)
            {
                return HttpNotFound();
            }

            var pdfresult = new PdfResult(tiket, "Print");
            return pdfresult;

        }
    }
}
