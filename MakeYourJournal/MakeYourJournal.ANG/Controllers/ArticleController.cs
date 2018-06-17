using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MakeYourJournal.DAL.Dtos;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourJournal.ANG.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            ArticleService = articleService;
            Mapper = mapper;
        }

        public IArticleService ArticleService { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        [ProducesResponseType(typeof(List<ArticleModel>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            return Ok(Mapper.Map<List<ArticleModel>>(ArticleService.GetArticles()));
        }

        [HttpGet]
        [Route("[action]/{AllTime}")]
        [ProducesResponseType(typeof(List<ArticleModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetByIssue(int AllTime)
        {
            return Ok(Mapper.Map<List<ArticleModel>>(ArticleService.GetArticlesByAllTimeNumber(AllTime)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleModel), (int)HttpStatusCode.OK)]
        public IActionResult Get(int Id)
        {
            return Ok(Mapper.Map<ArticleModel>(ArticleService.GetArticle(Id)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ArticleModel), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody]ArticleModel Article)
        {
            var created = ArticleService.AddArticle(Mapper.Map<MakeYourJournal.DAL.Entities.Article>(Article));
            return Ok(Mapper.Map<ArticleModel>(created));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            ArticleService.DeleteArticle(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody]ArticleModel Article)
        {
            var updated = ArticleService.UpdateArticle(Id, Mapper.Map<MakeYourJournal.DAL.Entities.Article>(Article));
            return Ok(Mapper.Map<ArticleModel>(updated));
        }
    }
}