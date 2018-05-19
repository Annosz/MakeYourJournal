using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}