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
    public class IssueController : Controller
    {

        public IssueController(IIssueService issueService, IMapper mapper)
        {
            IssueService = issueService;
            Mapper = mapper;
        }

        public IIssueService IssueService { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(Mapper.Map<List<IssueModel>>(IssueService.GetIssues()));
        }
    }
}