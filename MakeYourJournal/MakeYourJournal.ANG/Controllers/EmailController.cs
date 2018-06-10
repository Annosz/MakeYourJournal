using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MakeYourJournal.ANG.Services;
using MakeYourJournal.DAL.Dtos;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourJournal.ANG.Controllers
{
    [Produces("application/json")]
    [Route("api/Email")]
    public class EmailController : Controller
    {
        public EmailController(IEmailSender emailSender, IIssueService issueService, IMapper mapper)
        {
            EmailSender = emailSender;
            IssueService = issueService;
            Mapper = mapper;
        }

        public IEmailSender EmailSender { get; }
        public IIssueService IssueService { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult SendDeadlineReminder(int Id)
        {
            IssueModel Issue = Mapper.Map<IssueModel>(IssueService.GetIssue(Id));

            string message = string.Format("Dear {0},<br /><br />" +
                "The Lead Editor of {1} sends you a reminder of the next deadline.<br />" +
                "Please keep in mind that every article of the next issue must be ready until {2} midnight.<br /><br />" +
                "Best regards,<br />" +
                "MakeYourJournal Administrator",
                "Annosz", Issue.Name, Issue.Deadline.ToShortDateString());
            EmailSender.SendEmailAsync("tadamleanyfalu@gmail.com", "Deadline for issue", message);
            return NoContent();
        }
    }
}