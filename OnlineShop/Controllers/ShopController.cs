using Lista10.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Lista10.Controllers
{

    public class ShopController : Controller
    {
        private readonly ShopContext _context;

        public ShopController(ShopContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "DenyAdmin")]
        public async Task<IActionResult> Index(int? category)
        {
            
            if (category != null)
            {
                ViewBag.category = _context.Categories.Find(category).Name;
                ViewBag.catID = _context.Categories.Find(category).CategoryID;
                var productList = _context.Articles
                    .OrderBy(x => x.ArticleID)
                    .Where(x => x.CategoryID == category)
                    .Take(3)
                    .ToList();
                ViewBag.list = productList;
                ViewBag.lastID = productList.LastOrDefault().ArticleID;
          
            }
            else
            {
                ViewBag.catID = -1;
                ViewBag.category = "All Articles";
                var productList = _context.Articles
                    .OrderBy(x => x.ArticleID)
                    .Take(3)
                    .ToList();
                ViewBag.list = productList;
                ViewBag.lastID = productList.LastOrDefault().ArticleID;
            }

            return View(await _context.Categories.ToListAsync());
        }


    }
}
