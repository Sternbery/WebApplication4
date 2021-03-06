﻿using System;
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
    public class SurveyGiverController : Controller
    {
        private SurveySaysDB2Entities db = new SurveySaysDB2Entities();
       

        // GET: SurveyGiver
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("", "Account");

            if (!User.IsInRole("Giver")) {
                return RedirectToAction("Index", "SurveyTaker");
            }
            //var surveys = db.Surveys.Include(s => s.AspNetUser).Where(s => s.UserID == User.Identity.Name);
            var surveys = from s in db.Surveys
                        join u in db.AspNetUsers
                        on s.UserID equals u.Id
                        orderby s.DateCreated
                        where u.UserName == User.Identity.Name
                        select s;
            return View(surveys.ToList());
        }

        // GET: SurveyGiver/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("", "Account");

            if (!User.IsInRole("Giver"))
            {
                return RedirectToAction("Index", "SurveyTaker");
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

        // GET: SurveyGiver/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("", "Account");

            

            if (!User.IsInRole("Giver"))
            {
                if (!User.IsInRole("Taker"))
                    return View("~/Views/Shared/Error.cshtml",new HandleErrorInfo(new RowNotInTableException("Current User has No Roles"),"SurveyGiver","Create"));
                return RedirectToAction("Index", "SurveyTaker");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        // POST: SurveyGiver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyID,UserID,Name,DateCreated,TakerLimit,DateLimit")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", survey.UserID);
            return View(survey);
        }

        // GET: SurveyGiver/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", survey.UserID);
            return View(survey);
        }

        // POST: SurveyGiver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyID,UserID,Name,DateCreated,TakerLimit,DateLimit")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", survey.UserID);
            return View(survey);
        }

        // GET: SurveyGiver/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: SurveyGiver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
