using E_ticaret.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_ticaretWebApp.Components
{
    public class GetCategories : ViewComponent
    {
        DataContext context;

        public GetCategories(DataContext _context)
        {
            context = _context;
        }

        public IViewComponentResult Invoke()
        {            
            return View(context.Categories.ToList());
        }
    }
}
