//-----------------------------------------------------------------------
// <copyright file="INotesBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//---------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
   
    /// <summary>
    /// business layer Notes interface
    /// </summary>
    public interface INotesBL
    {
        /// <summary>
        /// create a note
        /// </summary>
        /// <param name="notesModel">notes model</param>
        /// <param name="userId">user id</param>
        /// <returns>return note entity</returns>
        public NotesEntity CreateNote(NotesModel notesModel, long userId);

        /// <summary>
        /// update a note
        /// </summary>
        /// <param name="notesModel">notes model</param>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user id</param>
        /// <returns>note entity</returns>
        public NotesEntity UpdateNote(UpdatNoteModel notesModel, long noteId, long userId);

        /// <summary>
        /// delete note
        /// </summary>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user id</param>
        /// <returns>return boolean value</returns>
        public bool DeleteNote(long noteId, long userId);

        /// <summary>
        /// fetch a note
        /// </summary>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user id</param>
        /// <returns>note entity</returns>
        public NotesEntity GetNote(long noteId, long userId);

        /// <summary>
        /// fetch notes by user id
        /// </summary>
        /// <param name="userId">user id></param>
        /// <returns>list of notes entity</returns>
        public List<NotesEntity> GetNotesByUserId(long userId);

        /// <summary>
        /// fetch all notes from database
        /// </summary>
        /// <returns>list of all notes from database</returns>
        public List<NotesEntity> GetAllNotes();

        /// <summary>
        /// note archive or not
        /// </summary>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user id</param>
        /// <returns>note entity</returns>
        public NotesEntity IsArchieveOrNot(long noteId, long userId);

        /// <summary>
        /// trash or not
        /// </summary>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user Id</param>
        /// <returns>note entity</returns>
        public NotesEntity IsTrashOrNot(long noteId, long userId);

        /// <summary>
        /// pinned or not
        /// </summary>
        /// <param name="noteId">notes Id</param>
        /// <param name="userId">user Id</param>
        /// <returns>note entity</returns>
        public NotesEntity IsPinnedOrNot(long noteId, long userId);

        /// <summary>
        /// upload image to cloud
        /// </summary>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user id</param>
        /// <param name="image">image</param>
        /// <returns>notes entity</returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);

        /// <summary>
        /// change color for note
        /// </summary>
        /// <param name="noteId">notes id</param>
        /// <param name="userId">user id</param>
        /// <param name="color">color</param>
        /// <returns>note entity</returns>
        public NotesEntity ChangeColour(long noteId, long userId, string color);
    }
}
