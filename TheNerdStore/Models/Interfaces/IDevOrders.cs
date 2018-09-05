using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNerdStore.Models.Interfaces
{
    public interface IDevOrders
    {
        Task<Order> PopulateOrderProducts(Cart cart, Order order);

        Task<Order> NewOrder(Cart cart, Order order);

        Task<Order> SaveOrder(Order order);

        Task<List<Order>> RecentOrders(int number);
    }
}
