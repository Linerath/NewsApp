using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Services
{
    public interface INewsProvider
    {
        Task<IEnumerable<News>> GetNews();
        Task<News> GetNews(int id);
        Task<int> AddNews(News news);
        Task DeleteNews(int id);
        Task UpdateNews(News news);
    }
}