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
        //[Route("/[controller]")]
        public IActionResult GetNews()
        {
            var model = newsProvider.GetNews();
            return new ObjectResult(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetNews(int id)
        {
            var model = newsProvider.GetNews(id);
            return new ObjectResult(model);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Post([FromBody]News news)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var id = newsProvider.AddNews(news);
            return new ObjectResult(id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]News news)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            newsProvider.DeleteNews(id);
        }
    }
}