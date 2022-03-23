//-----------------------------------------------------------------------
// <copyright file="NotesRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//--
namespace RepositoryLayer.Sevice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    ///  Notes Repository layer class
    /// </summary>
    public class NotesRL : INotesRL
    {
        /// <summary>The  object reference</summary>
        private readonly FundooContext fundooContext;

        /// <summary>The configuration object reference</summary>
        private readonly IConfiguration configuration;

        /// <summary>Initializes a new instance of the <see cref="NotesRL" /> class.</summary>
        /// <param name="fundooContext">The context object reference.</param>
        /// <param name="configuration">The configuration object reference</param>
        public NotesRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        /// <summary>Creates the note.</summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   Notes entity after note is created
        /// </returns>
        public NotesEntity CreateNote(NotesModel notesModel, long userId)
        {
            try 
            { 
                NotesEntity notes = new NotesEntity()
                {
                Title = notesModel.Title,
                Description = notesModel.Description,
                Reminder = notesModel.Reminder,
                Color = notesModel.Color,
                Image = notesModel.Image,
                IsTrash = notesModel.IsTrash,
                IsArchive = notesModel.IsArchive,
                IsPinned = notesModel.IsPinned,
                createdAt = notesModel.createdAt,
                modifiedAt = notesModel.modifiedAt,
                Id = userId
                };
                this.fundooContext.Notes.Add(notes);

                // Save Changes Made in the database
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Updates the note.</summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   Notes Entity of updated note
        /// </returns>
        public NotesEntity UpdateNote(UpdatNoteModel notesModel, long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId.
                var note = this.fundooContext.Notes.Where(u => u.NotesId == noteId && u.Id == userId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesModel.Title;
                    note.Description = notesModel.Description;
                    note.modifiedAt = notesModel.modifiedAt;

                    // Update database for given NoteId.
                    this.fundooContext.Notes.Update(note);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Deletes the note.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   return true if note is deleted
        /// </returns>
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                // Fetch details with the given noteId.
                var note = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (note != null)
                {
                    // Remove Note details from database
                    this.fundooContext.Notes.Remove(note);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Gets the note.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   note for matching note id
        /// </returns>
        public NotesEntity GetNote(long noteId, long userId)
        {
            try
            {
                // Fetch details with the given noteId.
                var note = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (note != null)
                { 
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Gets the notes by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   list of notes for user
        /// </returns>
        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                ////fetch all the notes with user id
                var notes = this.fundooContext.Notes.Where(n => n.Id == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Gets all notes.</summary>
        /// <returns>
        ///   list of notes from database
        /// </returns>
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                // Fetch All the details from Notes Table
                var notes = this.fundooContext.Notes.ToList();

                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Determines whether [is archive or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   notes entity if note is archive or not
        /// </returns>
        public NotesEntity IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();

                if (notes != null)
                {
                    if (notes.IsArchive == false)
                    {
                        notes.IsArchive = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsArchive = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Determines whether [is trash or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   notes entity if note is in trash or not
        /// </returns>
        public NotesEntity IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsTrash == false)
                    {
                        notes.IsTrash = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsTrash = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Determines whether [is pinned or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   Notes entity if note is pinned or not
        /// </returns>
        public NotesEntity IsPinnedOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsPinned == false)
                    {
                        notes.IsPinned = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsPinned = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>user entity after image upload</returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var note = this.fundooContext.Notes.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Account acc = new Account(this.configuration["Cloudinary:CloudName"], this.configuration["Cloudinary:ApiKey"], this.configuration["Cloudinary:ApiSecret"]);
                    Cloudinary cloud = new Cloudinary(acc);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };
                    var uploadResult = cloud.Upload(uploadParams);
                    note.Image = image.FileName;
                    this.fundooContext.Notes.Update(note);
                    int upload = this.fundooContext.SaveChanges();
                    if (upload > 0)
                    {
                        return note;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Changes the color.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   Change color of note
        /// </returns>
        public NotesEntity ChangeColour(long noteId, long userId, string color)
        {
            try
            {
                //change color for specific note
                var notes = this.fundooContext.Notes.FirstOrDefault(a => a.NotesId == noteId && a.Id == userId);
                if (notes != null)
                {
                    notes.Color = color;
                    this.fundooContext.SaveChanges();
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
