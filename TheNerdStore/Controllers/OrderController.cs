using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNerdStore.Models;
using TheNerdStore.Models.Interfaces;

namespace PuffyAmiYumi.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private IDevCarts _cart;
        private IDevOrders _context;
        public OrderController(IDevOrders context, IDevCarts cart, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _cart = cart;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }



        [AllowAnonymous]
        public IActionResult Shipping(Order order)
        {
            return View(order);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FinalizeOrder(Order order)
        {

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Cart cart = _cart.GetCart(user.Id);
            await _context.PopulateOrderProducts(cart, order);
            await _context.SaveOrder(order);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h2>Here is your Order!</h2>");
            sb.AppendLine("<p>Have a look at your purchases: ");

            foreach (OrderItem product in order.Products)
            {
                sb.Append($"Item: {product.ItemName} <br/>");
                sb.AppendLine($"Price: {product.Price} <br/>");
            }
            sb.AppendLine($"Total Price: ${order.Total}");
            sb.Append("</p>");

            await _emailSender.SendEmailAsync(user.Email, "Thank you for your order at Potpourri-R-Us!", sb.ToString());

            await _cart.EmptyCart(cart);
            return View("ThankYou");
        }
    }
}
