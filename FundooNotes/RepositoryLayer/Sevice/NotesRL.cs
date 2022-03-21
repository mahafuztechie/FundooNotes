using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Sevice
{
    public class NotesRL : INotesRL
    {
        public readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NotesRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public NotesEntity CreateNote(NotesModel notesModel, long UserId)
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
                Id = UserId
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

        public NotesEntity getNote(long noteId, long userId)
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

        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                //fetch all the notes with user id
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

        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var note = this.fundooContext.Notes.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Account acc = new Account(configuration["Cloudinary:CloudName"], configuration["Cloudinary:ApiKey"], configuration["Cloudinary:ApiSecret"]);
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

        public NotesEntity ChangeColour(long noteId, long userId, string color)
        {
            try
            {
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
