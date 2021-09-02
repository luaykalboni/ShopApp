using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopApp.Models;
using ShopApp.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ShopApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly shoppingsiteContext _context;

        public UsersController(shoppingsiteContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //adding a product to user's cart
        [Authorize]
        [HttpPost]
        public void Add_To_Cart(int proId, string userName)
        {
            var pro = _context.Product.Where(p => p.Id == proId).FirstOrDefault();
            if (pro != null)
            {
                var user =  _context.User.Where(u => u.Username == userName)
                    .Include(u => u.Cart)
                    .ThenInclude(c=>c.Products)
                    .FirstOrDefault();

                user.Cart.Products.Add(pro);
                user.Cart.TotalPrice += pro.Price;
                 _context.SaveChanges();
            }

        }
        //[HttpPost]
        //public void Remove_To_Cart(int proId, string userName)
        //{
        //    var pro = _context.Product.Where(p => p.Id == proId).FirstOrDefault();
        //    if (pro != null)
        //    {
        //        var user = _context.User.Where(u => u.Username == userName)
        //            .Include(u => u.Cart)
        //            .ThenInclude(c => c.Products)
        //            .FirstOrDefault();

        //        user.Cart.Products.Add(pro);
        //        user.Cart.TotalPrice += pro.Price;
        //        _context.SaveChanges();
        //    }

        //}

        //Users//Logout
        //Function  for sighning out
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Password")] User user)
        {

            var userExist = from u in _context.User
                            where u.Username == user.Username && u.Password == user.Password
                            select u;
           
            if (userExist.Count() > 0)
            {
                Signin(userExist.First());
                return RedirectToAction(nameof(Index), "home");
            }
            else
            {
                ViewData["Error"] = "Incorrect username/password, please try again";
            }


            return View(user);
        }

        //Users//Sighin
        //Function  for authentication 
        private async void Signin(User account)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Type.ToString()), 
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme
                );
            var authProperties = new AuthenticationProperties {

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10) 
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties); 
        }
    

        // GET: Users/Register
            public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Username,Password,FirstName,LastName,Address,Email,PhoneNumber")]  User user)
        {

            if (ModelState.IsValid)
            {
                var q = _context.User.FirstOrDefault(u => u.Username == user.Username);

                if (q == null)
                {
                    Cart c = new Cart()
                    {
                        User = user,
                        Products = new List<Product>()
                        
                    };
                    user.Cart = c;
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    var u = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                    Signin(u);
                    return RedirectToAction(nameof(Index),"Home");
                }
                else
                {
                    ViewData["Error"] = "Username already exists,please try another one.";
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,FirstName,LastName,Address,Email,PhoneNumber,Type")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
