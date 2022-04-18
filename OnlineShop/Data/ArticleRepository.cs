using Lista10.DAL;
using Lista10.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lista10.Data
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArticleRepository(ShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Article> Get(int id)
        {
            var article = await _context.Articles
            .Include(a => a.Category)
            .FirstOrDefaultAsync(m => m.ArticleID == id);
            if (article == null)
            {
                return null;
            }

            return article;
        }

        public async Task<List<Article>> GetAll()
        {
            var shopContext = _context.Articles.Include(a => a.Category);
            return await shopContext.ToListAsync();
        }

        public async Task<Article> Add(Article entity)
        {
                _context.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }

        public async Task<int> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return 2;
            }

            var imagePath = _webHostEnvironment.WebRootPath + article.ImageUrl;
            if (imagePath != (_webHostEnvironment.WebRootPath + "/image/nophoto.jpg"))
            {
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return 0;
        }

        public async Task<Article> Update(Article entity)
        {
            var category = await _context.Categories
               .FirstOrDefaultAsync(m => m.CategoryID == entity.CategoryID);
            if (category == null)
            {
                return null;
            }
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(entity.ArticleID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return await _context.Articles
            .Include(a => a.Category)
            .FirstOrDefaultAsync(m => m.ArticleID == entity.ArticleID); ; 
            }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleID == id);
        }

        public async Task<List<Article>> GetNextN(int lastId, int n, int categoryId)
        {
            if ( categoryId == -1)
            {
                  var res = _context.Articles
                    .Select(s => s)
                    .Include(a => a.Category)
                    .Where(s => s.ArticleID > lastId)
                    .OrderBy(s => s.ArticleID)
                    .Take(n);
                return await res.ToListAsync();
            }
            else
            {
                var res = _context.Articles
                  .Select(s => s)
                  .Include(a => a.Category)
                  .Where(s => s.ArticleID > lastId)
                  .Where(s => s.CategoryID == categoryId)
                  .OrderBy(s => s.ArticleID)
                  .Take(n);
                return await res.ToListAsync();
            }
          
        }
    }
}
