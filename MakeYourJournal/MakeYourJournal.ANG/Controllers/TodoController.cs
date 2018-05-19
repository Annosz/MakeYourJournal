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

namespace MakeYourJournal.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        public TodoController(ITodoService todoService, IMapper mapper)
        {
            TodoService = todoService;
            Mapper = mapper;
        }

        public ITodoService TodoService { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(Mapper.Map<List<TodoModel>>(TodoService.GetTodos()));
        }

        [HttpGet]
        [Route("[action]/{articleId}")]
        public IActionResult GetByArticle(int articleId)
        {
            return Json(Mapper.Map<List<TodoModel>>(TodoService.GetTodosByArticleId(articleId)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoModel), (int)HttpStatusCode.OK)]
        public IActionResult Get(int Id)
        {
            return Ok(Mapper.Map<TodoModel>(TodoService.GetTodo(Id)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoModel), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody]TodoModel Todo)
        {
            var created = TodoService.AddTodo(Mapper.Map<MakeYourJournal.DAL.Entities.Todo>(Todo));
            return Ok(Mapper.Map<TodoModel>(created));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            TodoService.DeleteTodo(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody]TodoModel Todo)
        {
            var updated = TodoService.UpdateTodo(Id, Mapper.Map<MakeYourJournal.DAL.Entities.Todo>(Todo));
            return Ok(Mapper.Map<TodoModel>(updated));
        }
    }
}