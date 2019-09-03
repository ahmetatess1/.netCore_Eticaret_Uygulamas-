using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_ticaret.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(GetCart());
        }

        public IActionResult AddToCart(int Id)
        {
            var product = _context.Products.FirstOrDefault(i => i.id == Id);

            if(product!=null)
            {
                Cart cart = GetCart();
                cart.AddProduct(product,1);
                HttpContext.Session.SetString("CartDetail1", JsonConvert.SerializeObject(cart));

            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int Id)
        {
            var product = _context.Products.FirstOrDefault(i => i.id == Id);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.DeleteProduct(product);
                HttpContext.Session.SetString("CartDetail1", JsonConvert.SerializeObject(cart));
            }
            return RedirectToAction("Index");
        }
        public Cart GetCart()
        {
            var value = HttpContext.Session.GetString("CartDetail1");
            
            //Nesneyi deserileştirilip çevirelim
            Cart cart= value == null ? null : JsonConvert.DeserializeObject<Cart>(value);
            if (cart == null)
            {
                cart = new Cart();
                return (Cart)cart;
            }
            else
                return cart;
        }
        public PartialViewResult GetSummary()
        {
            Cart cart = GetCart();            
            return PartialView(cart.CartLines.ToList());
        }
    }
}