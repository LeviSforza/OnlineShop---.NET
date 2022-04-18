using Lista10.DAL;
using Lista10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lista10.Data
{
    public class MemoryRepository : IRepository<Category>
    {
 
        private readonly ShopContext _context;
        public MemoryRepository(ShopContext context)
        {
            _context = context;
        }
   
        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            var category = await _context.Categories
               .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return null;
            }

            return category;
        }

        public async Task<Category> Add(Category entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> Delete(int id)
        {
            if (_context.Articles.Where(x => x.CategoryID == id).ToList().Count() != 0)
            {
                return 1;
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return 2;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return 0;
        }

        public async Task<Category> Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(entity.CategoryID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }

        public Task<List<Category>> GetNextN(int lastId, int n, int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
