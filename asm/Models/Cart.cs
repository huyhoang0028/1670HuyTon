using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace asm.Models
{
    public class CartItem
    {
        public ProductMaster ShoppingProduct { get; set; }
        public int ShoppingQuatity { get; set; }
    }

    public class Cart
    {
        public int CartId { get; set; }
        List<CartItem> items = new List<CartItem>();

        //get list of CartItem
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(ProductMaster product, int quatity = 1)
        {
            // if there is a product in the cart => product.ProductId is the product to check
            var item = Items.FirstOrDefault(x => x.ShoppingProduct.ProductId == product.ProductId);
            // if cart is empty (item == null) add product + quantity / if already have increase quantity
            if (item == null)
            {
                items.Add(new CartItem { ShoppingProduct = product, ShoppingQuatity = quatity });
            }
            else
            {
                item.ShoppingQuatity += quatity;
            }
        }

        public void UpdateQuatityProduct(int id, int quatity)
        {
            //if there is a product in the cart => update qutity
            var item = items.Find(x => x.ShoppingProduct.ProductId == id);
            if(item != null)
            {
                item.ShoppingQuatity = quatity;
            }
        }

        public double TotalMoney()
        {
            var total = items.Sum(x => x.ShoppingProduct.Price * x.ShoppingQuatity);
            return total;
        }
        public void Remove(int id)
        {
            items.RemoveAll(x => x.ShoppingProduct.ProductId == id);
        }
        public int TotlaQuantity()
        {
            return items.Sum(x => x.ShoppingQuatity);
        }
    }
}