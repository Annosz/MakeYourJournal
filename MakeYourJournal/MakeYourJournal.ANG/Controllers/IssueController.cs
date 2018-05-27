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
        [ProducesResponseType(typeof(List<IssueModel>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            return Ok(Mapper.Map<List<IssueModel>>(IssueService.GetIssues()).OrderByDescending(i => i.AllTime));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IssueModel), (int)HttpStatusCode.OK)]
        public IActionResult Get(int Id)
        {
            return Ok(Mapper.Map<IssueModel>(IssueService.GetIssue(Id)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IssueModel), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody]IssueModel Issue)
        {
            var created = IssueService.AddIssue(Mapper.Map<MakeYourJournal.DAL.Entities.Issue>(Issue));
            return Ok(Mapper.Map<IssueModel>(created));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            IssueService.DeleteIssue(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody]IssueModel Issue)
        {
            var updated = IssueService.UpdateIssue(Id, Mapper.Map<MakeYourJournal.DAL.Entities.Issue>(Issue));
            return Ok(Mapper.Map<IssueModel>(updated));
        }
    }
}