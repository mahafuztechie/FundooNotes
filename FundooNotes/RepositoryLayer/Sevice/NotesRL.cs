using CommonLayer.Model;
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
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
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

        public NotesEntity UpdateNote(NotesModel notesModel, long noteId)
        {
            try
            {
                // Fetch All the details with the given noteId.
                var note = this.fundooContext.Notes.Where(u => u.NotesId == noteId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesModel.Title;
                    note.Description = notesModel.Description;
                    note.Color = notesModel.Color;
                    note.Image = notesModel.Image;
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
    }
}
