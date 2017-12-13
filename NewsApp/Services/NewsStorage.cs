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

        public async Task<int> AddNews(News news)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO news (heading, text, creationDate, category) VALUES(@Heading, @Text, @CreationDate, @Category); SELECT LAST_INSERT_ID()";
                return await db.QueryFirstOrDefaultAsync<int>(sqlQuery, news);
            }
        }

        public async Task DeleteNews(int id)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM news WHERE id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }

        public async Task<IEnumerable<News>> GetNews()
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return await db.QueryAsync<News>("SELECT * FROM news"); /*ToList();*/
            }
        }

        public async Task<News> GetNews(int id)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<News>("SELECT * FROM news WHERE id = @id", new { id });
            }
        }

        public async Task UpdateNews(News news)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE news SET heading = @Heading, text = @Text, creationDate = @CreationDate, category = @Category WHERE id = @Id";
                await db.ExecuteAsync(sqlQuery, news);
            }
        }
    }
}