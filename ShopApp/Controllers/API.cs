using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Controllers
{
    public class API : Controller
    {
        private readonly shoppingsiteContext _context;

        public API(shoppingsiteContext context)
        {
            _context = context;

        }


        [HttpGet]
        public string[] get_categories()
        {
            var a = _context.Category.Select(u => u.Name).ToArray();

            return a;
        }


        [HttpGet]
        public string[] get_products()
        {
            var a = _context.Product.Select(u => u.Name).ToArray();

            return a;
        }


        [HttpGet]
        public string[] get_carts()
        {
            var usersNames = _context.Cart.Include(c => c.User).Select(u => u.User.Username).ToArray();

            return usersNames;
        }
        [HttpGet]
        public string[] get_users()
        {
            var usersNames = _context.Cart.Include(c => c.User).Select(u => u.User.Username).ToArray();

            return usersNames;

        }

        [HttpGet]
        public string[] get_products_inStock_inRange(int min, int max)
        {
            var a = _context.Product.Where(p => p.IsinStock == true && (p.Price > min && p.Price < max)).Select(u => u.Name).ToArray();

            return a;
        }

        [HttpGet]
        public int getProductQty(string userName)
        {
            var user = _context.User
                .Include(u => u.Cart)
                .ThenInclude(c => c.lines)
                .Where(u => u.Username == userName)
                .FirstOrDefault();

            if (user != null)
            {
                return user.Cart.getProductQty();
            }

            return 0;


        }



    }
}
