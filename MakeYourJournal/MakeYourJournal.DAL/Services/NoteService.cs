using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MakeYourJournal.DAL.Services
{
    public class NoteService : INoteService
    {
        public NoteService(JournalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public JournalDbContext DbContext { get; }


        public IEnumerable<Note> GetNotes()
        {
            return DbContext.Notes.Include(n => n.Article).ToList();
        }

        public IEnumerable<Note> GetNotesByArticleId(int articleId)
        {
            return DbContext.Notes.Where(n => n.ArticleId == articleId).Include(n => n.Article).ToList();
        }

        public Note GetNote(int Id)
        {
            return DbContext.Notes.Include(n => n.Article).FirstOrDefault(n => n.Id == Id)
                ?? throw new EntityNotFoundException("Note not found");
        }

        
        public Note AddNote(Note Note)
        {
            DbContext.Notes.Add(Note);

            DbContext.SaveChanges();

            return GetNote(Note.Id);
        }

        public Note UpdateNote(int NoteId, Note Note)
        {
            Note.Id = NoteId;
            var entry = DbContext.Attach(Note);
            entry.State = EntityState.Modified;

            try
            {
                DbContext.SaveChanges();
                return GetNote(Note.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Note not found");
            }
        }

        public void DeleteNote(int NoteId)
        {
            DbContext.Notes.Remove(new Note { Id = NoteId });

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Note not found");
            }
        }
        
    }
}
