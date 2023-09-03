using crudmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudmvc.Controllers
{
    public class CategoryController : Controller
    {
        private AvanuDataContext av = new AvanuDataContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(av.Categories.ToList());
        }

        private Category GetCategory(int id)
        {
            return av.Categories.SingleOrDefault(c => c.CategoryId == id);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
           
            return View(GetCategory(id));
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category cd)
        {
            try
            {
                // TODO: Add insert logic here
                Category cv = new Category();
                cv.CategoryId = av.Categories.Max(c => c.CategoryId) + 1;
                cv.CategoryName = cd.CategoryName;
                av.Categories.InsertOnSubmit(cv);
                av.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {

            return View(GetCategory(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category ed)
        {
            try
            {
                // TODO: Add update logic here
                Category ce = GetCategory(id);
                ce.CategoryName = ed.CategoryName;
                av.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Category cd = GetCategory(id);
            av.Categories.DeleteOnSubmit(cd);
            av.SubmitChanges();
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
