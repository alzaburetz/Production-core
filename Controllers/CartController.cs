using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Production.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Production.Controllers
{
    public class CartController : Controller
    {
        private readonly DBContext _context;

        public CartController(DBContext context)
        {
            _context = context;
        }
        public async void makeOrder() {
        var cart = _context.Cart.Where(x => x.user_id == User.Identity.Name)
            .ToArray();
            string[] items = new string[cart.Count()];
            float sum = 0.0f;
            int i = 0;
            foreach (Cart item in cart) {
                Cart input = new Cart();
                input.id = item.id;
                input.item_name = item.item_name;
                sum += item.sum;
                input.user_id = item.user_id;
                input.sum = item.sum;
                input.items_amount = item.items_amount;
                object json  = JsonConvert.SerializeObject(input);
                items[i] = json.ToString();
                i++;
                
            }
            object final = "[";
            for (i = 0; i < items.Length; i++) {
                final += items[i];
                if (i != items.Length-1) {
                    final += ",";
                }
            }
            final += "]";
            Orders order = new Orders();
            order.items = final.ToString();
            order.summa = sum;
             _context.Add(order);
             _context.Cart.RemoveRange(cart);
            await _context.SaveChangesAsync();
            
            
        }
        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var test = _context.Cart;
            return View(await test.Where(x => x.user_id == User.Identity.Name.ToString()).ToListAsync());
        }

        // GET: Cart/Details/5
        

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            var product = _context.Product.SingleOrDefault(x => x.p_name == cart.item_name);
            product.amount += cart.items_amount;
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.id == id);
        }
    }
}
