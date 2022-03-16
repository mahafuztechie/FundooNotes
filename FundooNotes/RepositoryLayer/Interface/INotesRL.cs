using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNote(NotesModel notesModel, long UserId);
        public NotesEntity UpdateNote(UpdatNoteModel notesModel, long noteId, long userId);
        public bool DeleteNote(long noteId, long userId);

        public NotesEntity getNote(long noteId, long userId);
        public List<NotesEntity> GetNotesByUserId(long userId);
        public List<NotesEntity> GetAllNotes();
        public NotesEntity IsArchieveOrNot(long noteId, long userId);
        public NotesEntity IsTrashOrNot(long noteId, long userId);
        public NotesEntity IsPinnedOrNot(long noteId, long userId);
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);
        public NotesEntity ChangeColour(long noteId, long userId, string color);
    }
}
