using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asm.CustomFilters;
using asm.Models;

namespace asm.Controllers
{
    public class ProductController : Controller
    {
        ProductDbcontext ctx;
        public ProductController()
        {
            ctx = new ProductDbcontext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var Products = ctx.ProductMasters.ToList();
            return View(Products);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            var Product = new ProductMaster();
            return View(Product);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Create(ProductMaster p)
        {
            try
            {
                if(p.ImageUpload != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(p.ImageUpload.FileName);
                    var extension = Path.GetExtension(p.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    p.Image = "~/content/img/" + fileName;
                    p.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/content/img/"), fileName));
                }
                ctx.ProductMasters.Add(p);
                ctx.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }            
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int id)
        {
            return View(ctx.ProductMasters.Where(s => s.ProductId == id).FirstOrDefault());
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            return View(ctx.ProductMasters.Where(s => s.ProductId == id).FirstOrDefault());
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Delete(int id, ProductMaster p)
        {
            try
            {
                p = ctx.ProductMasters.Where(s => s.ProductId == id).FirstOrDefault();
                ctx.ProductMasters.Remove(p);
                ctx.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            return View(ctx.ProductMasters.Where(s => s.ProductId == id).FirstOrDefault());
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Edit(int id, ProductMaster p)
        {
            try
            {
                if (p.ImageUpload != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(p.ImageUpload.FileName);
                    var extension = Path.GetExtension(p.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    p.Image = "~/content/img/" + fileName;
                    p.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/content/img/"), fileName));
                }
                ctx.Entry(p).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AuthLog(Roles = "Sales Executive")]
        public ActionResult SaleProduct()
        {
            ViewBag.Message = "This View is designed for the Sales Executive to Sale Product.";
            return View();
        }

    }

}
