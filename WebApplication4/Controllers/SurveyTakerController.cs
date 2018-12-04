using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Views
{
    public class SurveyTakerController : Controller
    {
        private SurveySaysDB2Entities db = new SurveySaysDB2Entities();

        // GET: SurveyTaker
        public ActionResult  Index()
        {
            var surveys = db.Surveys.Include(s => s.AspNetUser); 
            return View(surveys.ToList());

        }
        //Get: SurveyTaker/StartSurvey/5
        public ActionResult StartSurvey()
        {
            
         
     
            SurveyResponse sr = new SurveyResponse();
            ViewBag.SurveyResponseID = sr.ResponseID;
            return View();
        }

        // GET: SurveyTaker/Details/5
        public ActionResult Details(int? id)
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

        // GET: SurveyTaker/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: SurveyTaker/Create
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

        // GET: SurveyTaker/Take/5
        public async Task<ActionResult> Take(int? id)
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

            SurveyResponse surveyresponse = new SurveyResponse();
            surveyresponse.SurveyID = survey.SurveyID;
            surveyresponse.UserID = db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First().Id;
            surveyresponse.DateTaken = System.DateTime.Now;

            db.SurveyResponses.Add(surveyresponse);
            await db.SaveChangesAsync();

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", survey.UserID);
           
            return View(survey);
        }

        // POST: SurveyTaker/Take/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Take([Bind(Include = "SurveyID,UserID,Name,DateCreated,TakerLimit,DateLimit")] Survey survey)
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

        // GET: SurveyTaker/MultipleAnswer/5
        public async Task<ActionResult> MultipleAnswer(int? id)
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
            ViewBag.Question = surveyQuestion.Text;
            ViewBag.QuestionTypeID = new SelectList(db.TypeEnums, "QuestionTypeID", "TypeName", surveyQuestion.QuestionTypeID);

            return View(surveyQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MultipleAnswer([Bind(Include = "MAAID,ResponseID,QuestionID,Answer")] SurveyMAA MAA)
        {
            if (ModelState.IsValid)
            {
                db.SurveyMAAs.Add(MAA);
                await db.SaveChangesAsync();
                return RedirectToAction("Take/" + ViewBag.SurveyID + "SurveyTaker");
            }

            return View(MAA);
        }

        public async Task<ActionResult> MultipleChoice(int? id)
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
            ViewBag.Question = surveyQuestion.Text;

            return View(surveyQuestion);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MultipleChoice([Bind(Include = "MCAID,ResponseID,QuestionID,Answer")] SurveyMCA MCA)
        {
            if (ModelState.IsValid)
            {
                

                db.SurveyMCAs.Add(MCA);
                await db.SaveChangesAsync();
                return RedirectToAction("Take/" + ViewBag.SurveyID + "SurveyTaker");
            }

            return View(MCA);
        }


        public async Task<ActionResult> ShortAnswer(int? id)
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
            ViewBag.Question = surveyQuestion.Text;

            return View(surveyQuestion);
        }



        // GET: SurveyTaker/Delete/5
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

        // POST: SurveyTaker/Delete/5
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
