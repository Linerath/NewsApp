using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private INewsProvider newsProvider;

        public NewsController(INewsProvider newsProvider)
        {
            this.newsProvider = newsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var model = await newsProvider.GetNews();
            return new ObjectResult(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var model = await newsProvider.GetNews(id);
            return new ObjectResult(model);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]News news)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var id = await newsProvider.AddNews(news);
            return new ObjectResult(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]News news)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            if (id != news.Id)
            {
                return new BadRequestResult();
            }
            await newsProvider.UpdateNews(news);
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await newsProvider.DeleteNews(id);
            return new OkResult();
        }
    }
}