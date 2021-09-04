using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopApp.Models;
using ShopApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShopApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly shoppingsiteContext _context;

        public CartsController(shoppingsiteContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {

            var shoppingsiteContext = await _context.Cart.Include(c => c.User).ToListAsync();
            return View(shoppingsiteContext);
        }

        [Authorize]
        public async Task<IActionResult> Usercart(string userName)
        {

            var shoppingsiteContext = await _context.Cart
                .Include(c => c.lines)
                .ThenInclude(l=>l.product)
                .ThenInclude(c => c.Category)
                .Include(u=>u.User).Where(u => u.User.Username == userName).FirstOrDefaultAsync();
            return View(shoppingsiteContext);
        }

        [HttpPost]
        public async Task<User> Remove_From_Cart(int proId, string userName)

        {
            var pro = await _context.Product.Where(p => p.Id == proId).FirstOrDefaultAsync();
            if (pro != null)
            {
                var user = await _context.User.Where(u => u.Username == userName)
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.lines)
                    .ThenInclude(l => l.product)
                    .FirstOrDefaultAsync();

                user.Cart.lines.ForEach(l =>
                {
                    if (l.productId == proId)
                    {
                        if (l.qty > 1)
                        {
                            l.qty--;
                            l.total_price_line -= pro.Price;
                            user.Cart.TotalPrice -= pro.Price;
                        }
                        else
                        {
                            user.Cart.TotalPrice -= l.total_price_line;
                            l.product = null;
                            user.Cart.lines.Remove(l);
                            _context.SaveChanges();
                        }
                    }
                });
                await _context.SaveChangesAsync();
                return user;
            }
            return null;


        }


        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TotalPrice")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TotalPrice")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
