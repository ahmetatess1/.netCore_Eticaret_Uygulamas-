using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_ticaretWebApp.Models;
using E_ticaret.DataAccess;

namespace E_ticaretWebApp.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;            
        }

        public IActionResult Index()
        {
            var urunler = _context.Products.Where(i => i.IsHome && i.IsApproved)
                .Select(i => new Product()
                {
                    id = i.id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image ?? "1.jpg",
                    CategoryId = i.CategoryId

                }).ToList();
            return View(urunler);
        }

        public IActionResult Details(int id)
        {
            return View(_context.Products.Where(i => i.id == id).FirstOrDefault());
        }
        public IActionResult List(int? id)
        {

            var urunler = _context.Products.Where(i => i.IsApproved)
                .Select(i => new Product()
                {
                    id = i.id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image ?? "1.jpg",
                    CategoryId = i.CategoryId

                }).AsQueryable();
            if(id!=null)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }

            return View(urunler.ToList());
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
