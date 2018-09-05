using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNerdStore.Data;
using TheNerdStore.Models.Interfaces;

namespace TheNerdStore.Models
{
    public class DevOrders : IDevOrders
    {
        private CustomDbContext _context;
        public DevOrders(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<Order> NewOrder(Cart cart, Order order)
        {
            order.Products = new List<OrderItem>();
            await PopulateOrderProducts(cart, order);
            return order;
        }

        public async Task<Order> PopulateOrderProducts(Cart cart, Order order)
        {
            order.Date = DateTime.Now.ToString("MMM d, yyyy (ddd) @ HH:mm tt");
            order.Products = new List<OrderItem>();
            foreach (CartItem item in cart.CartItems)
            {
                OrderItem OrderItem = new OrderItem();
                OrderItem.ItemID = item.ProductID;
                OrderItem.OrderID = order.ID;
                OrderItem.ItemName = item.ProductName;
                OrderItem.Price = item.Price;
                OrderItem.TotalCost = OrderItem.Price * item.Quantity;
                OrderItem.Quantity = item.Quantity;
                order.Products.Add(OrderItem);
                order.Total += OrderItem.TotalCost;
            }
            return order;
        }

        public async Task<Order> SaveOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> RecentOrders(int x)
        {
            List<Order> lastNOrders = await _context.Orders.Skip(Math.Max(0, _context.Orders.Count() - x)).ToListAsync();
            return lastNOrders;
        }
    }
}
