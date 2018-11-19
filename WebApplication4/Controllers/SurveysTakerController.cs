using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Views
{
    public class SurveysTakerController : Controller
    {
        private SurveySaysDB2Entities db = new SurveySaysDB2Entities();

        // GET: SurveysTaker
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("","Account");

            if (!User.IsInRole("Taker"))
            {
                return RedirectToAction("Index", "SurveyGiver");
            }

            var surveys = db.Surveys.Include(s => s.AspNetUser);
            return View(surveys.ToList());
        }

        // GET: SurveysTaker/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("", "Account");

            if (!User.IsInRole("Taker"))
            {
                return RedirectToAction("Index", "SurveyGiver");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: SurveysTaker/Create
       

        // POST: SurveysTaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        // GET: SurveysTaker/Edit/5


        // POST: SurveysTaker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: SurveysTaker/Delete/5


        // POST: SurveysTaker/Delete/5


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
