using Lista10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lista10.Data
{
    public interface IRepositoryArticle
    {
        IEnumerable<Article> Articles { get; }
        Article this[int id] { get; }
        Article AddArticle(Article article);
        Article UpdateArticle(Article article);
        void DeleteArticle(int id);
    }
}
