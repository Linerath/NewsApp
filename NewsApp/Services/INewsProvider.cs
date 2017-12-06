using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Services
{
    public interface INewsProvider
    {
        IEnumerable<News> GetNews();
        News GetNews(int id);
        void AddNews(News news);
        void DeleteNews(int id);
        void UpdateNews(News news);
    }
}