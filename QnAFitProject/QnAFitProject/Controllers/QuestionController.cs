using QnAFitProject.Models;
using QnAFitProject.Repository;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QnAFitProject.Controllers
{
    public class QuestionController : Controller
    {
        /*
         * 2 fields. The first one is the interface repository for question
         * The second field is for my DbContext where I instance it
         */
        private IQuestionRep<Question> questionRep = null;
        private FitnessDbContext db = new FitnessDbContext();

        // Constructor
        public QuestionController()
        {
            // I set questionRep field to QuestionRep with the included Question
            //that comes from the model class Question
            questionRep = new QuestionRep<Question>();
        }

        // GET: Question
        /*
         * In my index method I create a function where you can filter through categories
         */
        public ActionResult Index(int? SelectedCategory)
        {
            var category = db.Category.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedCategory = new SelectList(category, "CategoryID", "Name", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            IQueryable<Question> question = db.Question
               .Where(c => !SelectedCategory.HasValue || c.CategoryID == categoryID)
               .OrderBy(d => d.QuestionID)
               .Include(d => d.Category);
            var sql = question.ToString();
            return View(question.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            /* 
             * calls the method when making a get request for create
             * But doesn't select the DropDownList
            */
            CategoryDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    questionRep.Insert(question);
                    questionRep.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log error
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            //Calls the method DropDownList
            CategoryDropDownList(question.CategoryID);
            return View(question);
        }

        // GET
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                /* Here I instance a new Http status with the status code
                * defined for http. HttpStatusCode.BadRequest means that
                * the request could not be understood by the server
                */
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = questionRep.GetById(Id);

            if (question == null)
            {
                //return the value if the requested page is not found
                return HttpNotFound();
            }

            /* 
             * calls the method when making a get request for edit
             * But doesn't select the DropDownList
            */
            CategoryDropDownList(question.CategoryID);
            return View(question);
        }

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (question == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    questionRep.Update(question);
                    questionRep.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log error
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }

            //Call the DropDownList method
            CategoryDropDownList(question.CategoryID);
            return View(question);
        }

        public ActionResult Details(int Id)
        {
            var question = questionRep.GetById(Id);
            return View(question);
        }

        /*
         * This method gets a list of all the categories sorted by name
         * SelectList creates a collection for a drop down list and transfer
         * the collection to the view through the ViewBag
         * The methods takes 4 parameters
         */
        private void CategoryDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from d in db.Category
                                orderby d.Name
                                select d;
            ViewBag.CategoryID = new SelectList(categoryQuery, "CategoryID", "Name", selectedCategory);
        }

        // GET
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                /* Here I instance a new Http status with the status code
                * defined for http. HttpStatusCode.BadRequest means that
                * the request could not be understood by the server
                */
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = questionRep.GetById(Id);

            if (question == null)
            {
                /* 
                * calls the method when making a get request for edit
                * But doesn't select the DropDownList
                */
                return HttpNotFound();
            }

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            var question = questionRep.GetById(Id);
            questionRep.Delete(Id);
            questionRep.Save();
            return RedirectToAction("Index");
        }
    }
}