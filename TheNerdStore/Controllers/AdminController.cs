using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNerdStore.Models;
using TheNerdStore.Models.Interfaces;

namespace TheNerdStore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IDevProducts _products;
        private readonly IDevOrders _orders;
        private readonly IConfiguration Configuration;

        public AdminController(IDevProducts products, IConfiguration configuration, IDevOrders orders)
        {
            _products = products;
            _orders = orders;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets all products and displays them to the view
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("ViewAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _products.GetAllProducts();
            return View(products);
        }

        /// <summary>
        /// Displays the view of the create page
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Allows user to add a product with the product properties added
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID, Name, SKU, Price, Description, ImgUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _products.CreateProduct(product);
                return RedirectToAction("ViewAll");
            }
            return View(product);
        }

        /// <summary>
        /// Finds the product by ID and throws it back to the View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _products.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("ViewAll");
            }
            return View(product);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var foundProduct = await _products.GetProductById(id);

            if (foundProduct == null) return NotFound();

            return View(foundProduct);
        }

        /// <summary>
        /// Updates an existing product, otherwise user is redirected back to ViewAllProducts
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Throws the found product to the view.
        /// Otherwise, redirects the user back to view all products
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID, Name, SKU, Price, Description, ImgUrl")] Product product)
        {
            if (id != product.ID)
            {
                return RedirectToAction("ViewAll");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _products.UpdateProductAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                        return RedirectToAction("ViewAll");
                }
            }
            return View(product);
        }

        /// <summary>
        /// Displays the view for deleting an item.  Method finds the item and
        /// passes it to the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>user to view all products if the product id doesn't match.
        /// Otherwise, throws the product found to the View
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewAll");
            }

            Product product = await _products.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("ViewAll");
            }
            return View(product);
        }

        /// <summary>
        /// Deletes a product by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>redirects user to view all products</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmAsync(int id)
        {
            await _products.DeleteProduct(id);
            return RedirectToAction("ViewAll");
        }

        public IActionResult RecentOrders()
        {
            List<Order> recentOrders = _orders.RecentOrders(20).Result;

            return View(recentOrders);
        }
    }
}
