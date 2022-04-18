using Lista10.DAL;
using Lista10.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lista10.Controllers
{

    public class CartController : Controller
    {
        private readonly ShopContext _context;

        public CartController(ShopContext context)
        {
            _context = context;
        }

        [Authorize]
        [Authorize(Policy = "DenyAdmin")]
        public async Task<IActionResult> Order()
        {
            List<Article> productList = new List<Article>();
            Dictionary<int, string> amount = new Dictionary<int, string>();
            int sum = 0, prodNumb = 0;
            if (Request.Cookies != null)
            {
                foreach (Article art in _context.Articles)
                {
                    if (Request.Cookies[art.ArticleID.ToString()] != null)
                    {
                        sum += art.Price * Int32.Parse(Request.Cookies[art.ArticleID.ToString()]);
                        prodNumb += Int32.Parse(Request.Cookies[art.ArticleID.ToString()]);
                        productList.Add(art);
                        amount.Add(art.ArticleID, Request.Cookies[art.ArticleID.ToString()]);
                    }
                }
                if (prodNumb == 0)
                {
                    ViewBag.message = "Your shopping cart is empty!";
                    ViewBag.red = "red";
                    ViewBag.disabled = "disabled";
                }
                else
                {
                    ViewBag.message = "You have " + prodNumb + " products in your shopping cart!";
                }
            }
            else
            {
                ViewBag.message = "Your shopping cart is empty!";
                ViewBag.red = "red";
                ViewBag.disabled = "disabled";
            }

            ViewBag.sum = sum;
            ViewBag.list = productList;
            ViewBag.amount = amount;
            return View(await _context.Categories.ToListAsync());
        }


        [Authorize]
        [Authorize(Policy = "DenyAdmin")]
        public IActionResult Confirmed([Bind("name")] string name, [Bind("surname")] string surname,
            [Bind("phone")] string phone, [Bind("postcode")] string postcode, [Bind("city")] string city,
            [Bind("street")] string street, [Bind("house")] string house, [Bind("payment")] string payment)
        {
            List<Article> productList = new List<Article>();
            if (Request.Cookies != null)
            {
                foreach (Article art in _context.Articles)
                {
                    if (Request.Cookies[art.ArticleID.ToString()] != null)
                    {
                        productList.Add(art);
                    }
                }
            }

            foreach (Article article in productList)
            {
                if (article == null)
                {
                    return NotFound();
                }

                if (Request.Cookies[article.ArticleID.ToString()] != null)
                {
                    Response.Cookies.Delete(article.ArticleID.ToString());
                }
            }

            ViewData["name"] = name;
            ViewData["surname"] = surname;
            ViewData["phone"] = phone;
            ViewData["city"] = city;
            ViewData["street"] = street;
            ViewData["house"] = house;
            ViewData["postcode"] = postcode;
            ViewData["payment"] = payment;

            return View();
        }

        [Authorize]
        [Authorize(Policy = "DenyAdmin")]
        public async Task<IActionResult> Cart()
        {
            List<Article> productList = new List<Article>();
            Dictionary<int, string> amount = new Dictionary<int, string>();
            int sum = 0, prodNumb = 0;
            if (Request.Cookies != null)
            {
                foreach (Article art in _context.Articles)
                {
                    if (Request.Cookies[art.ArticleID.ToString()] != null)
                    {
                        sum += art.Price * Int32.Parse(Request.Cookies[art.ArticleID.ToString()]);
                        prodNumb += Int32.Parse(Request.Cookies[art.ArticleID.ToString()]);
                        productList.Add(art);
                        amount.Add(art.ArticleID, Request.Cookies[art.ArticleID.ToString()]);
                    }
                }
                if (prodNumb == 0)
                {
                    ViewBag.message = "Your shopping cart is empty!";
                    ViewBag.red = "red";
                    ViewBag.disabled = "disabled";
                }
                else
                {
                    ViewBag.message = "You have " + prodNumb + " products in your shopping cart!";
                }
            }
            else
            {
                ViewBag.message = "Your shopping cart is empty!";
                ViewBag.red = "red";
                ViewBag.disabled = "disabled";
            }

            ViewBag.sum = sum;
            ViewBag.list = productList;
            ViewBag.amount = amount;
            return View(await _context.Categories.ToListAsync());
        }

        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (Request.Cookies[id.ToString()] == null)
            {
                SetCookie(id, 1, 7);
            }
            else
            {
                var cookie = Request.Cookies[id.ToString()];
                int val = Int32.Parse(cookie);
                SetCookie(id, val + 1, 7);
            }

            return RedirectToAction("Cart", "Cart");
        }

        public async Task<IActionResult> AddStay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (Request.Cookies[id.ToString()] == null)
            {
                SetCookie(id, 1, 7);
            }
            else
            {
                var cookie = Request.Cookies[id.ToString()];
                int val = Int32.Parse(cookie);
                SetCookie(id, val + 1, 7);
            }

            return RedirectToAction("Index", "Shop");
        }


        public async Task<IActionResult> Minus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (Request.Cookies[id.ToString()] != null)
            {
                var cookie = Request.Cookies[id.ToString()];
                int val = Int32.Parse(cookie);
                if (val - 1 == 0)
                {
                    Response.Cookies.Delete(id.ToString());
                }
                else
                {
                    SetCookie(id, val - 1, 7);
                }
            }
            return RedirectToAction("Cart", "Cart");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (Request.Cookies[id.ToString()] != null)
            {
                Response.Cookies.Delete(id.ToString());
            }
            return RedirectToAction("Cart", "Cart");
        }

        public void SetCookie(int? key, int value, int? numberOfDays = null)
        {
            if (key != null)
            {
                CookieOptions option = new CookieOptions();
                if (numberOfDays.HasValue)
                    option.Expires = DateTime.Now.AddDays(numberOfDays.Value);
                Response.Cookies.Append(key.ToString(), value.ToString(), option);
            }

        }

    }
}
