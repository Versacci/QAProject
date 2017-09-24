using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QnAFitProject.Models;
using QnAFitProject.Repository;
using System.Data.Entity.Infrastructure;

namespace QnAFitProject.Controllers
{
    public class AnswerController : Controller
    {
        /*
         * 2 fields. The first one is the interface repository for answer
         * The second field is for my DbContext where I instance it
         */
        private IAnswerRep<Answer> answerRep = null;
        private FitnessDbContext db = new FitnessDbContext();

        public AnswerController()
        {
            // I set questionRep field to QuestionRep with the included Question
            //that comes from the model class Question
            answerRep = new AnswerRep<Answer>();
        }

        // GET: Answer
        public ActionResult Index()
        {
            //Here I create a variable answer and use the GetAll method from my repository
            // to get a list of my answers
            var answer = answerRep.GetAll();
            return View(answer);
        }

        // GET: Answer/Create
        public ActionResult Create()
        {
            /* 
             * calls the method when making a get request for create
             * But doesn't select the DropDownList
            */
            SelectQuestion();
            return View();
        }

        // POST: Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Answer answer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    answerRep.Insert(answer);
                    answerRep.Save();
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log error
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            //Calls the method DropDownList
            SelectQuestion(answer.QuestionID);
            return View(answer);
        }

        // GET: Answer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                /* Here I instance a new Http status with the status code
                * defined for http. HttpStatusCode.BadRequest means that
                * the request could not be understood by the server
                */
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = answerRep.GetById(id);

            if (answer == null)
            {
                //return the value if the requested page is not found
                return HttpNotFound();
            }

            /* 
             * calls the method when making a get request for edit
             * But doesn't select the DropDownList
            */
            SelectQuestion(answer.QuestionID);
            return View(answer);
        }

        // POST: Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Answer answer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    answerRep.Update(answer);
                    answerRep.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log error
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }

            //Call the DropDownList method
            SelectQuestion(answer.QuestionID);
            return View(answer);
        }

        // GET: Answer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        /*
         * This method gets a list of all the question sorted by name
         * SelectList creates a collection for a drop down list and transfer
         * the collection to the view through the ViewBag
         * The methods takes 4 parameters
         */
        private void SelectQuestion(object selectedQuestion = null)
        {
            var questionQuery = from d in db.Question
                                orderby d.Title, d.Text
                                select d;
            ViewBag.QuestionID = new SelectList(questionQuery, "QuestionID", "Title", selectedQuestion);
        }

        public ActionResult Answers(int? QuestionID)
        {
            var answer = db.Question.Where(q => q.QuestionID == QuestionID).Select(i => i.Text).FirstOrDefault();
            return Json(answer);
        }

        // GET: Answer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                /* Here I instance a new Http status with the status code
                * defined for http. HttpStatusCode.BadRequest means that
                * the request could not be understood by the server
                */
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = answerRep.GetById(id);

            if (answer == null)
            {
                /* 
                * calls the method when making a get request for edit
                * But doesn't select the DropDownList
                */
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var answer = answerRep.GetById(id);
            answerRep.Delete(id);
            answerRep.Save();
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
