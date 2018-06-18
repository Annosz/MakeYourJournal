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
using Microsoft.AspNetCore.Mvc.Versioning;

namespace MakeYourJournal.ANG.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [HttpGet, MapToApiVersion("1.0")]
        [Route("[action]/{Volume}/{Number}")]
        [ProducesResponseType(typeof(List<ArticleModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetByIssue(int Volume, int Number)
        {
            return Ok(Mapper.Map<List<ArticleModel>>(ArticleService.GetArticlesByIssueVolumeAndNumber(Volume, Number)));
        }

        [HttpGet, MapToApiVersion("2.0")]
        [Route("[action]/{AllTimeNumber}")]
        [ProducesResponseType(typeof(List<ArticleModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetByIssue(int AllTimeNumber)
        {
            return Ok(Mapper.Map<List<ArticleModel>>(ArticleService.GetArticlesByAllTimeNumber(AllTimeNumber)));
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