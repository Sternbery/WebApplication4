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
    public class SurveyMCQsController : Controller
    {
        private SurveySaysDB2Entities db = new SurveySaysDB2Entities();

        // GET: SurveyMCQs
        public async Task<ActionResult> Index()
        {
            var surveyMCQs = db.SurveyMCQs.Include(s => s.SurveyQuestion);
            return View(await surveyMCQs.ToListAsync());
        }

        // GET: SurveyMCQs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyMCQ surveyMCQ = await db.SurveyMCQs.FindAsync(id);
            if (surveyMCQ == null)
            {
                return HttpNotFound();
            }
            return View(surveyMCQ);
        }

        // GET: SurveyMCQs/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
                ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text");
            else
            {
                ViewBag.QuestionID = id;
                ViewBag.QuestionText = db.SurveyQuestions.Where(q => q.QuestionID == id).First().Text;
            }
            return View();
        }

        // POST: SurveyMCQs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MCQID,QuestionID,ChoiceOrder,Text")] SurveyMCQ surveyMCQ)
        {
            if (ModelState.IsValid)
            {
                db.SurveyMCQs.Add(surveyMCQ);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit/" + surveyMCQ.QuestionID, "SurveyQuestions");
            }

            ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text", surveyMCQ.QuestionID);
            return View(surveyMCQ);
        }

        // GET: SurveyMCQs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyMCQ surveyMCQ = await db.SurveyMCQs.FindAsync(id);
            if (surveyMCQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text", surveyMCQ.QuestionID);
            return View(surveyMCQ);
        }

        // POST: SurveyMCQs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MCQID,QuestionID,ChoiceOrder,Text")] SurveyMCQ surveyMCQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyMCQ).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text", surveyMCQ.QuestionID);
            return View(surveyMCQ);
        }

        // GET: SurveyMCQs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyMCQ surveyMCQ = await db.SurveyMCQs.FindAsync(id);
            if (surveyMCQ == null)
            {
                return HttpNotFound();
            }
            return View(surveyMCQ);
        }

        // POST: SurveyMCQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveyMCQ surveyMCQ = await db.SurveyMCQs.FindAsync(id);
            db.SurveyMCQs.Remove(surveyMCQ);
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
