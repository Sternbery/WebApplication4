﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "Your application FAQ page.";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "Your privacy policy.";

            return View();
        }

        public ActionResult AboutGiver()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactGiver()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQGiver()
        {
            ViewBag.Message = "Your application FAQ page.";

            return View();
        }

        public ActionResult PrivacyPolicyGiver()
        {
            ViewBag.Message = "Your privacy policy.";

            return View();
        }
    }
}