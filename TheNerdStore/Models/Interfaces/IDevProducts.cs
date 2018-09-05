using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNerdStore.Models.Interfaces
{
    public interface IDevProducts
    {
        Task<Product> CreateProduct(Product product);

        Task<Product> GetProductById(int? id);

        Task<List<Product>> GetProduct();

        void UpdateProductAsync(Product product);

        Task DeleteProduct(int ID);

        Task<List<Product>> GetAllProducts();
    }
}
