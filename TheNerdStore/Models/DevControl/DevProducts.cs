using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNerdStore.Data;
using TheNerdStore.Models.Interfaces;

namespace TheNerdStore.Models
{
    public class DevProducts : IDevProducts
    {

        private CustomDbContext _context;

        public DevProducts(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetProduct()
        {
            var list = await _context.Products.ToListAsync();
            return list;
        }

        //public async Product GetProductById(int? id) => await _context.Products.Single<Product>(p => p.ID == id);

        public async void UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task DeleteProduct(int id)
        {
            Product product = _context.Products.Single(p => p.ID == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int? id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.ID == id);
            return product;
        }
    }
}
