//-----------------------------------------------------------------------
// <copyright file="NotesBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//--
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;
   
    /// <summary>
    /// business layer Notes Class
    /// </summary>
    public class NotesBL : INotesBL
    {
        /// <summary>
        /// object reference for notes RL
        /// </summary>
        private readonly INotesRL notesRL;

        /// <summary>
        /// constructor with dependency injection
        /// </summary>
        /// <param name="notesRL">notesRL</param>
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        /// <summary>
        /// create note
        /// </summary>
        /// <param name="notesModel">notesModel</param>
        /// <param name="userId">UserId</param>
        /// <returns>Notes Entity</returns>
        public NotesEntity CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                return this.notesRL.CreateNote(notesModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// update a note
        /// </summary>
        /// <param name="notesModel">notesModel</param>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">UserId</param>
        /// <returns>Notes Entity</returns>
        public NotesEntity UpdateNote(UpdatNoteModel notesModel, long noteId, long userId)
        {
            try
            {
                return this.notesRL.UpdateNote(notesModel, noteId, userId);
            }
            catch (Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// delete a note
        /// </summary>
        /// <param name="noteId">noteId</param>
        /// <param name="userId">UserId</param>
        /// <returns>bool value</returns>
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                return this.notesRL.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// fetch all Notes
        /// </summary>
        /// <returns>list of notes</returns>
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                return this.notesRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// fetch notes by User Id
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>list of notes</returns>
        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                return this.notesRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// fetch one note by id
        /// </summary>
        /// <param name="noteId">noteId</param>
        /// <param name="userId">userId</param>
        /// <returns>returns a note matching note id</returns>
        public NotesEntity GetNote(long noteId, long userId)
        {
            try
            {
                return this.notesRL.GetNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// note is archive or not
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="userId">user id</param>
        /// <returns>note</returns>
        public NotesEntity IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsArchieveOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// note is trash or not
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="userId">user id</param>
        /// <returns>note</returns>
        public NotesEntity IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsTrashOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// note is pinned or not
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="userId">user id</param>
        /// <returns>note</returns>
        public NotesEntity IsPinnedOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsPinnedOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// upload image to cloud
        /// </summary>
        /// <param name="noteId">noteId</param>
        /// <param name="userId">userId</param>
        /// <param name="image">image</param>
        /// <returns>note</returns>
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

        /// <summary>
        /// change a note color
        /// </summary>
        /// <param name="noteId">noteId</param>
        /// <param name="userId">userId</param>
        /// <param name="color">color</param>
        /// <returns>note</returns>
        public NotesEntity ChangeColour(long noteId, long userId, string color)
        {
            try
            {
                return this.notesRL.ChangeColour(noteId, userId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}