using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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

        public NotesEntity UpdateNote(UpdatNoteModel notesModel, long noteId, long userId)
        {
            try
            {
                return notesRL.UpdateNote(notesModel, noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                return notesRL.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                return notesRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                return notesRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity getNote(long noteId, long userId)
        {
            try
            {
                return notesRL.getNote(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsArchieveOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsTrashOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public NotesEntity IsPinnedOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsPinnedOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                return this.notesRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
