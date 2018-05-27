using MakeYourJournal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Services
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotes();
        IEnumerable<Note> GetNotesByArticleId(int articleId);
        Note GetNote(int Id);
        Note AddNote(Note Note);
        Note UpdateNote(int NoteId, Note Note);
        void DeleteNote(int NoteId);
    }
}
