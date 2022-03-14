using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public NotesEntity CreateNote(NotesModel notesModel, long UserId)
        {
            try
            {
                return notesRL.CreateNote(notesModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity UpdateNote(NotesModel notesModel, long noteId)
        {
            try
            {
                return notesRL.UpdateNote(notesModel, noteId);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
