// <copyright file="INotesRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;

    /// <summary>
    ///   Notes interface for repository layer
    /// </summary>
    public interface INotesRL
    {
        /// <summary>Creates the note.</summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return note after a note is created
        /// </returns>
        public NotesEntity CreateNote(NotesModel notesModel, long userId);

        /// <summary>Updates the note.</summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return the updated note
        /// </returns>
        public NotesEntity UpdateNote(UpdatNoteModel notesModel, long noteId, long userId);

        /// <summary>Deletes the note.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return true if note is deleted
        /// </returns>
        public bool DeleteNote(long noteId, long userId);

        /// <summary>Gets the note.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   a note is returned matching note id
        /// </returns>
        public NotesEntity GetNote(long noteId, long userId);

        /// <summary>Gets the notes by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   list of notes are returned for user
        /// </returns>
        public List<NotesEntity> GetNotesByUserId(long userId);

        /// <summary>Gets all notes.</summary>
        /// <returns>
        ///   list of notes from database
        /// </returns>
        public List<NotesEntity> GetAllNotes();

        /// <summary>Determines whether [is archive or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return a not if archive or not
        /// </returns>
        public NotesEntity IsArchieveOrNot(long noteId, long userId);

        /// <summary>Determines whether [is trash or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return a note if in trash or not
        /// </returns>
        public NotesEntity IsTrashOrNot(long noteId, long userId);

        /// <summary>Determines whether [is pinned or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return note if pinned  or not
        /// </returns>
        public NotesEntity IsPinnedOrNot(long noteId, long userId);

        /// <summary>Uploads the image.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        ///   returns note after uploading image
        /// </returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);

        /// <summary>Changes the color.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   return note after changing color
        /// </returns>
        public NotesEntity ChangeColour(long noteId, long userId, string color);
    }
}
