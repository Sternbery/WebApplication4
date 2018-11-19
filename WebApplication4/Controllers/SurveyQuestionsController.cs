using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class SurveyQuestionsController : Controller
    {
        private SurveySaysDB2Entities db = new SurveySaysDB2Entities();

        // GET: SurveyQuestions
        public async Task<ActionResult> Index()
        {
            var surveyQuestions = db.SurveyQuestions.Include(s => s.Survey).Include(s => s.TypeEnum);
            return View(await surveyQuestions.ToListAsync());
        }

        // GET: SurveyQuestions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return HttpNotFound();
            }
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Create
        public ActionResult Create()
        {
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "UserID");
            ViewBag.QuestionTypeID = new SelectList(db.TypeEnums, "QuestionTypeID", "TypeName");
            return View();
        }

        // POST: SurveyQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuestionID,SurveyID,Text,QuestionTypeID")] SurveyQuestion surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                db.SurveyQuestions.Add(surveyQuestion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "UserID", surveyQuestion.SurveyID);
            ViewBag.QuestionTypeID = new SelectList(db.TypeEnums, "QuestionTypeID", "TypeName", surveyQuestion.QuestionTypeID);
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "UserID", surveyQuestion.SurveyID);
            ViewBag.QuestionTypeID = new SelectList(db.TypeEnums, "QuestionTypeID", "TypeName", surveyQuestion.QuestionTypeID);
            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuestionID,SurveyID,Text,QuestionTypeID")] SurveyQuestion surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "UserID", surveyQuestion.SurveyID);
            ViewBag.QuestionTypeID = new SelectList(db.TypeEnums, "QuestionTypeID", "TypeName", surveyQuestion.QuestionTypeID);
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return HttpNotFound();
            }
            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            db.SurveyQuestions.Remove(surveyQuestion);
            await db.SaveChangesAsync();
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
