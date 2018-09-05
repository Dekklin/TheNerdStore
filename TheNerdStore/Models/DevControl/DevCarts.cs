﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNerdStore.Data;
using TheNerdStore.Models.Interfaces;

namespace TheNerdStore.Models.DevControl
{
    public class DevCarts : IDevCarts
    {
        private CustomDbContext _context;
        public DevCarts(CustomDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Takes in the 3 parameters and combines them into one object, known as the CartItem
        /// </summary>
        /// <param name="user"> The Current User that is logged in </param>
        /// <param name="cart"> The Cart Associated with the User that is logged in </param>
        /// <param name="product"> The Product that the user clicked on to Add to their cart </param>
        /// <returns> The updated Cart, which is attached to a User, and it is full of Prducts/CartItems </returns>
        public Cart AddProductToCart(ApplicationUser user, Cart cart, Product product)
        {
            try
            {
                CartItem item = cart.CartItems.FirstOrDefault(p => p.ProductID == product.ID);
                item.Quantity++;
            }
            catch (Exception e)
            {
                CartItem item = new CartItem();
                item.CartID = cart.ID;
                item.ProductID = product.ID;
                item.ProductName = product.ProductName;
                item.ProductIMG = product.ImageURL;
                item.Quantity = 1;
                item.Price = product.Price;
                cart.CartItems.Add(item);
                _context.CartItems.Add(item);
            }
            finally
            {
                _context.SaveChanges();

            }

            return cart;
        }

        /// <summary>
        /// Grabs the cart associated with the specific User
        /// </summary>
        /// <param name="id">The UserTag, an ID from a User</param>
        /// <returns>Cart, has a UserID from the param, if the User doesn't have an existing cart it creates one</returns>
        public Cart GetCart(string id)
        {
            var cart = _context.Carts.FirstOrDefault(x => x.UserTag == id);
            if (cart == null)
            {
                cart = new Cart();
                cart.UserTag = id;
                _context.Carts.Add(cart);
            }
            cart.CartItems = GetCartItems(cart.ID);
            _context.SaveChanges();
            return cart;
        }

        /// <summary>
        /// Grabs the CartItems that are associated with a specific CartID
        /// </summary>
        /// <param name="id">A CartID</param>
        /// <returns>List of CartItems</returns>
        public List<CartItem> GetCartItems(int id)
        {
            try
            {
                var x = _context.CartItems.OrderByDescending(a => a.CartID == id).ToList();
            }
            catch (Exception)
            {
                return new List<CartItem>();
            }
            return _context.CartItems.OrderByDescending(a => a.CartID == id).ToList();
        }

        /// <summary>
        /// In order to create a CartItem we must attach a product with that CartItem, here we grab the product
        /// </summary>
        /// <param name="id"> a product ID</param>
        /// <returns>Product</returns>
        public Product GetProduct(int id)
        {
            return _context.Products.First(f => f.ID == id);
        }
        /// <summary>
        /// Deletes a specific CartItem from a specific Cart
        /// </summary>
        /// <param name="id">Id of a cartItem</param>
        public void DeleteCartItem(int id)
        {
            var item = _context.CartItems.FirstOrDefault(f => f.ID == id);
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }
        public async Task<Cart> EmptyCart(Cart cart)
        {
            foreach (CartItem item in cart.CartItems)
            {
                _context.CartItems.Remove(item);
            }
            cart.CartItems = new List<CartItem>();
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
