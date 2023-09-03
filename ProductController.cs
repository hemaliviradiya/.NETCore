using crudmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudmvc.Controllers
{
    public class ProductController : Controller
    {
        private AvanuDataContext av = new AvanuDataContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(av.ProductCats.ToList());
        }

        private ProductCat GetProductCat(int id)
        {
            return av.ProductCats.SingleOrDefault(p => p.Pid == id);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View(GetProductCat(id));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            foreach(var ca in av.Categories)
            {
                ls.Add(new SelectListItem { Text = ca.CategoryName, Value = ca.CategoryId.ToString() });
            }
            ViewBag.ls = ls;
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductCat pc)
        {
            try
            {
                // TODO: Add insert logic here
                ProductCat pd = new ProductCat
                {
                    Pid = av.ProductCats.Max(p => p.Pid) + 1,
                    Pname = pc.Pname,
                    CategoryId = pc.CategoryId,
                    Rate_ = pc.Rate_,
                    Des = pc.Des
                };
                av.ProductCats.InsertOnSubmit(pd);
                av.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            foreach (var ca in av.Categories)
            {
                ls.Add(new SelectListItem { Text = ca.CategoryName, Value = ca.CategoryId.ToString() });
            }
            ViewBag.ls = ls;
            return View(GetProductCat(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductCat uppro)
        {
            try
            {
                // TODO: Add update logic here

                ProductCat productCat = GetProductCat(id);
                productCat.Pname = uppro.Pname;
                productCat.CategoryId = uppro.CategoryId;
                productCat.Rate_ = uppro.Rate_;
                productCat.Des = uppro.Des;

                av.ProductCats.Append(productCat);
                av.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductCat p = GetProductCat(id);
            av.ProductCats.DeleteOnSubmit(p);
            av.SubmitChanges();
            return View();
        }

        // POST: Product/Delete/5
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
