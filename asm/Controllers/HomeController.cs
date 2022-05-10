using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asm.Models;

namespace asm.Controllers
{
    public class HomeController : Controller
    {
        ProductDbcontext ctx; 
        public HomeController()
        {
            ctx = new ProductDbcontext();
        }

        public ActionResult Index(string name)
        {
            return View(ctx.ProductMasters.Where(x => x.ProductName.Contains(name) || name == null).ToList());
        }

       
    }
}