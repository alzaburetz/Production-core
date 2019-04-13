using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Production.Models;
using System.Diagnostics;

namespace Production.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DBContext _context;

        public ProductsController(DBContext context)
        {
            _context = context;
        }

        public async void reduceMaterials(int amountt, int id) {
            var result = await _context.Materials.FindAsync(id);
            int reduce = result.amount - amountt;
            result.amount = reduce;
            await _context.SaveChangesAsync();
        }

        public async void AddToCart([FromQuery] float sum, [FromQuery] int amount,[FromQuery] string item_name) { 
            Cart cart = new Cart();
            cart.sum = sum;
            cart.user_id = User.Identity.Name;
            cart.items_amount = amount;
            cart.item_name = item_name;
            _context.Cart.Add(cart);
            try {
            var prod = _context.Product.SingleOrDefault(x => x.p_name == item_name);
            prod.amount -= amount;
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }   
            await _context.SaveChangesAsync();
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name == "admin@admin.net") {
            return View(await _context.Product.ToListAsync());
            } else {
                return View(await _context.Product.Where(p => p.amount > 0).ToListAsync());
            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (User.Identity.Name == "admin@admin.net") {
            return View();
            } else {

                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,p_name, description,amount,price,material_id,material")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                reduceMaterials(product.material, product.material_id);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.Name == "admin@admin.net") {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
            } else {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,p_name, description,amount,price,material_id,material")] Product product)
        {
            if (id != product.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.Name == "admin@admin.net") {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
            } else {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public virtual bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.id == id);
        }
        [HttpGet, ActionName("GetMaterials")]
        public virtual JsonResult GetMaterials() {
            var materials = _context.Materials.ToList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(materials, Newtonsoft.Json.Formatting.Indented);
            return new JsonResult(json);
        }
    }
}
