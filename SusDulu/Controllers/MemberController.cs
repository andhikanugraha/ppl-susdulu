﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using SusDulu.Models;

namespace SusDulu.Controllers
{
    public class MemberController : Controller
    {
        private SusDuluDbContext db = new SusDuluDbContext();

        //
        // GET: /Member/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Member/Register/

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Member/Register/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            user.Id_user = getNewID();
            if (ModelState.IsValid)
            {
                db.user.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // POST: /Member/Login/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (WebSecurity.Login(user.email, user.password))
            {
                // Sucessful login message
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Failed login message
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Member/Logout/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            // Logout message
            return RedirectToAction("Index", "Home");
        }

        private List<String> getUserList()
        {
            var userList = new List<String>();

            var userQuery = from user in db.user
                            select user.Id_user;
            userList.AddRange(userQuery);

            return userList;
        }

        private String generateRandomID()
        {
            var chars = "0123456789";
            var random = new Random();
            return new String(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private String getNewID()
        {
            List<String> userList = getUserList();
            String retval = "";
            do
            {
                retval = generateRandomID();
            }
            while (userList.Exists(s => s.Equals(retval)));

            return retval;
        }
    }
}