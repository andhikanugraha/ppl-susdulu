using SusDulu.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SusDulu.Controllers
{
    public class PaymentController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        //
        // GET: /Payment/

        public ActionResult Index()
        {
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

            //generate ID Ticket auto-increment
            List<int> tickIDList = new List<int>();
            var tickIDQry = from t in db.Tickets
                            select t.ID;
            tickIDList.AddRange(tickIDQry.Distinct());
            int lastElmt = tickIDList.Last();
            int newID = lastElmt + 1;

            //check no duplicate seat
            if (HasDuplicates(seatL))
            {
                //ViewBag.errSeat = "You must choose different seat";
                this.Session["errSeat"] = "You must choose different seat";
                return RedirectToAction("create", new FlightOption { id_flight1 = id_flight1, id_flight2 = id_flight2, Sum = sum });
            }

            int IDUser = WebSecurity.CurrentUserId;
            Debug.WriteLine("IDUSER: "+IDUser);

            for (int k = 0; k < emailL.Count; k++)
            {
                if (ModelState.IsValid)
                {
                    //get certain IDUser
                    List<int> userIDList = new List<int>();

                    Ticket newTicket = new Ticket(newID, IDUser, id_flight1, emailL.ElementAt(k), firstL.ElementAt(k), middleL.ElementAt(k), lastL.ElementAt(k), addressL.ElementAt(k), phoneL.ElementAt(k), genderL.ElementAt(k), cityL.ElementAt(k), provinceL.ElementAt(k), postcodeL.ElementAt(k), classL.ElementAt(k), priceL.ElementAt(k), seatL.ElementAt(k));
                    CommitDB(id_flight1, newTicket);

                    newTicket = new Ticket(newID, IDUser, id_flight2, emailL.ElementAt(k), firstL.ElementAt(k), middleL.ElementAt(k), lastL.ElementAt(k), addressL.ElementAt(k), phoneL.ElementAt(k), genderL.ElementAt(k), cityL.ElementAt(k), provinceL.ElementAt(k), postcodeL.ElementAt(k), classL.ElementAt(k), priceL.ElementAt(k), seatL.ElementAt(k));
                    CommitDB(id_flight2,newTicket);
                }
            }

            return RedirectToAction("Index","Home");
        }

        private void CommitDB(int IDFlight, Ticket ticket)
        {
            if (IDFlight != 0)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
            }
            return;
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

    }
}
