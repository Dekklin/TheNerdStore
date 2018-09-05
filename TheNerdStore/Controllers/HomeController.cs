using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNerdStore.Models;
using TheNerdStore.Models.Interfaces;

namespace TheNerdStore.Controllers
{
    public class HomeController : Controller
    {
            private IDevProducts _context;

            public HomeController(IDevProducts context)
            {
                _context = context;
            }

            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Create(Product product)
            {
                _context.CreateProduct(product);
                return View();
            }

            public IActionResult GetByID(int id)
            {
                _context.GetProductById(id);
                return View();
            }

            public IActionResult GetAll()
            {
                _context.GetProduct();
                return View();
            }

            public IActionResult Update(Product product)
            {
                _context.UpdateProductAsync(product);
                return View();
            }
        }
}
