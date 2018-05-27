using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MakeYourJournal.DAL.Dtos;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourJournal.ANG.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        public NoteController(INoteService noteService, IMapper mapper)
        {
            NoteService = noteService;
            Mapper = mapper;
        }

        public INoteService NoteService { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(Mapper.Map<List<NoteModel>>(NoteService.GetNotes()));
        }

        [HttpGet]
        [Route("[action]/{articleId}")]
        public IActionResult GetByArticle(int articleId)
        {
            return Json(Mapper.Map<List<NoteModel>>(NoteService.GetNotesByArticleId(articleId)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NoteModel), (int)HttpStatusCode.OK)]
        public IActionResult Get(int Id)
        {
            return Ok(Mapper.Map<NoteModel>(NoteService.GetNote(Id)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(NoteModel), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody]NoteModel Note)
        {
            var created = NoteService.AddNote(Mapper.Map<MakeYourJournal.DAL.Entities.Note>(Note));
            return Ok(Mapper.Map<NoteModel>(created));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            NoteService.DeleteNote(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody]NoteModel Note)
        {
            var updated = NoteService.UpdateNote(Id, Mapper.Map<MakeYourJournal.DAL.Entities.Note>(Note));
            return Ok(Mapper.Map<NoteModel>(updated));
        }
    }
}