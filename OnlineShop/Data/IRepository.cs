using Lista10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Lista10.Data
{
     public interface IRepository<T>
     {
         Task<List<T>> GetAll();
         Task<T> Get(int id);
         Task<T> Add(T entity);
         Task<T> Update(T entity);
         Task<int> Delete(int id);

         Task<List<T>> GetNextN(int lastId, int n, int categoryId);
    }
}
