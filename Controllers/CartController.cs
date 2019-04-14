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
        public virtual async Task<JsonResult> makeOrder() {
        var cart = await _context.Cart.Where(x => x.user_id == User.Identity.Name).ToListAsync();
            
            float sum = 0.0f;
            foreach (Cart item in cart) {
                sum += item.sum;
            }
            Orders order = new Orders();
            order.items = JsonConvert.SerializeObject(cart);
            order.summa = sum;
            await _context.AddAsync(order);
             _context.Cart.RemoveRange(cart);
            await _context.SaveChangesAsync();
            if (cart != null) {
                return new JsonResult("Successfully added");
            } else {
                return new JsonResult("Error");
            }
            
        }
        // GET: Cart
        public virtual async Task<List<Cart>> getIndex()
        {
            return await _context.Cart.Where(x => x.user_id == User.Identity.Name.ToString()).ToListAsync();
        }
        public async Task<IActionResult> Index()
        {
            return View(await getIndex());
        }
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
        public virtual Cart getDeletable(int id)
        {
            var cart =  _context.Cart.FirstOrDefault(m => m.id == id);
            if (cart.id == 0) {
                throw new NullReferenceException();
            } else {
            return cart;
            }
        }

        // POST: Cart/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = getDeletable(id);
            var product =  await _context.Product.SingleOrDefaultAsync(x => x.p_name == cart.item_name);
            product.amount += cart.items_amount;
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public virtual bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.id == id);
        }
    }
}
