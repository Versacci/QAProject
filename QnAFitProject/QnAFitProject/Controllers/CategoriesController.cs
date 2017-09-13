using QnAFitProject.Models;
using QnAFitProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QnAFitProject.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryRep<Category> categoryRep = null;
        private FitnessDbContext db = new FitnessDbContext();

        public CategoriesController()
        {
            this.categoryRep = new CategoryRep<Category>();
        }

        // GET: Categories
        public ActionResult Index(string searchString)
        {
            var category = categoryRep.GetAll();

            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRep.Insert(category);
                categoryRep.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Edit(int Id)
        {
            var category = categoryRep.GetById(Id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRep.Update(category);
                categoryRep.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Details(int Id)
        {
            var category = categoryRep.GetById(Id);
            return View(category);
        }

        public ActionResult Delete(int Id)
        {
            var category = categoryRep.GetById(Id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            var category = categoryRep.GetById(Id);
            categoryRep.Delete(Id);
            categoryRep.Save();
            return RedirectToAction("Index");
        }
    }
}