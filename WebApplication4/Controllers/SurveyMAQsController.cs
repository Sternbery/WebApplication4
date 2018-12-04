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
    public class SurveyMAQsController : Controller
    {
        private SurveySaysDB2Entities db = new SurveySaysDB2Entities();

        // GET: SurveyMAQs
        public async Task<ActionResult> Index()
        {
            var surveyMAQs = db.SurveyMAQs.Include(s => s.SurveyQuestion).OrderBy(s => s.QuestionID);
            return View(await surveyMAQs.ToListAsync());
        }

        // GET: SurveyMAQs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyMAQ surveyMAQ = await db.SurveyMAQs.FindAsync(id);
            if (surveyMAQ == null)
            {
                return HttpNotFound();
            }
            return View(surveyMAQ);
        }

        // GET: SurveyMAQs/Create
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

        // POST: SurveyMAQs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MAQID,QuestionID,ChoiceOrder,Text")] SurveyMAQ surveyMAQ)
        {
            if (ModelState.IsValid)
            {
                db.SurveyMAQs.Add(surveyMAQ);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit/"+surveyMAQ.QuestionID,"SurveyQuestions");
            }

            ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text", surveyMAQ.QuestionID);
            return View(surveyMAQ);
        }

        // GET: SurveyMAQs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyMAQ surveyMAQ = await db.SurveyMAQs.FindAsync(id);
            if (surveyMAQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text", surveyMAQ.QuestionID);
            return View(surveyMAQ);
        }

        // POST: SurveyMAQs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAQID,QuestionID,ChoiceOrder,Text")] SurveyMAQ surveyMAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyMAQ).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.SurveyQuestions, "QuestionID", "Text", surveyMAQ.QuestionID);
            return View(surveyMAQ);
        }

        // GET: SurveyMAQs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyMAQ surveyMAQ = await db.SurveyMAQs.FindAsync(id);
            if (surveyMAQ == null)
            {
                return HttpNotFound();
            }
            return View(surveyMAQ);
        }

        // POST: SurveyMAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveyMAQ surveyMAQ = await db.SurveyMAQs.FindAsync(id);
            int qid = surveyMAQ.QuestionID;
            db.SurveyMAQs.Remove(surveyMAQ);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit/" + qid, "SurveyQuestions");
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
