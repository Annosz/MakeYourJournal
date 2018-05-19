using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get()
        {
            return Json(Mapper.Map<List<ArticleModel>>(ArticleService.GetArticles()));
        }
    }
}