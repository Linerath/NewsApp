using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsApp.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace NewsApp.Services
{
    public class NewsStorage : INewsProvider
    {
        private string connectionString;

        public NewsStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MySqlConnectionString");
        }

        public int AddNews(News news)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO news (heading, text, creationDate, category) VALUES(@Heading, @Text, @CreationDate, @Category)";
                var id = db.Query<int>(sqlQuery, news);
                return id;
                int? newsId = db.Query<int>(sqlQuery, news).FirstOrDefault();
                return newsId.Value;
            }
        }

        public void DeleteNews(int id)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM news WHERE id = @id";
                db.ExecuteAsync(sqlQuery, new { id });
            }
        }

        public IEnumerable<News> GetNews()
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return db.Query<News>("SELECT * FROM news").ToList();
            }
        }

        public News GetNews(int id)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return db.Query<News>("SELECT * FROM news WHERE id = @id", new { id }).FirstOrDefault();
            }
        }

        public void UpdateNews(News news)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE news SET heading = @Heading, text = @Text, creationDate = @CreationDate, category = @Category WHERE id = @Id";
                db.Execute(sqlQuery, news);
            }
        }
    }
}