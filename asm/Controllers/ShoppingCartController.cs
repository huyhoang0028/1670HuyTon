using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asm.Models;

namespace asm.Controllers
{
    public class ShoppingCartController : Controller
    {
        ProductDbcontext ctx;
        public ShoppingCartController()
        {
            ctx = new ProductDbcontext();
        }

        // create cart 
        public Cart GetCart()
        {
            
            // if cart null create new cart
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //  add item to cart 
        public ActionResult AddToCart(int id)
        {
            //if have item add item in cart 
            var p = ctx.ProductMasters.SingleOrDefault(x => x.ProductId == id);
            if(p != null)
            {
                GetCart().Add(p);
            }                
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        //show cart
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);

        }

        public ActionResult UpdateQuatityCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            // add id from form to product_id
            int product_id =int.Parse(form["Product_Id"]);          
            int quantity = int.Parse(form["Quatity"]);
            cart.UpdateQuatityProduct(product_id, quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int totlaItem = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)
            {
                totlaItem = cart.TotlaQuantity();
            }
            ViewBag.InfoCart = totlaItem;
            return PartialView("BagCart");
        }
    }
}