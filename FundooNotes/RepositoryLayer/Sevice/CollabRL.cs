// <copyright file="CollabRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Sevice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;
    
    /// <summary>
    ///   Collaborator RL
    /// </summary>
    public class CollabRL : ICollabRL
    {
        /// <summary>The  object reference</summary>
        private readonly FundooContext fundooContext;

        /// <summary>Initializes a new instance of the <see cref="CollabRL" /> class.</summary>
        /// <param name="fundooContext">The reference object.</param>
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>Adds the collaborator.</summary>
        /// <param name="collaboratorModel">The collaborator model.</param>
        /// <returns>
        ///   Add Collaborator
        /// </returns>
        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel)
        {
            try
            {
                CollaboratorEntity collaboration = new CollaboratorEntity();
                var user = this.fundooContext.User.Where(e => e.Email == collaboratorModel.CollabEmail).FirstOrDefault();
                var notes = this.fundooContext.Notes.Where(e => e.NotesId == collaboratorModel.NoteID && e.Id == collaboratorModel.Id).FirstOrDefault();
                if (notes != null && user != null)
                {
                    collaboration.NotesId = collaboratorModel.NoteID;
                    collaboration.CollabEmail = collaboratorModel.CollabEmail;
                    collaboration.Id = collaboratorModel.Id;
                    this.fundooContext.Collab.Add(collaboration);
                    var result = this.fundooContext.SaveChanges();
                    return collaboration;
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

        /// <summary>Removes the collaborator.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collaborator identifier.</param>
        /// <returns>
        ///  Remove Collaborator 
        /// </returns>
        public CollaboratorEntity RemoveCollab(long userId, long collabId)
        {
            try
            {
                // Fetch All the details from Collab Table by user id and collab id.
                var data = this.fundooContext.Collab.FirstOrDefault(d => d.Id == userId && d.CollabId == collabId);
                if (data != null)
                {
                    this.fundooContext.Collab.Remove(data);
                    this.fundooContext.SaveChanges();
                    return data;
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

        /// <summary>Gets the by note identifier.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   Get By Note Id
        /// </returns>
        public IEnumerable<CollaboratorEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                // Fetch All the details from Collab Table by note id.
                var data = this.fundooContext.Collab.Where(c => c.NotesId == noteId && c.Id == userId).ToList();
                if (data != null)
                {
                    return data;
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

        /// <summary>Gets all collaborator.</summary>
        /// <returns>
        ///  Get all collaborator
        /// </returns>
        public IEnumerable<CollaboratorEntity> GetAllCollab()
        {
            try
            {
                // Fetch All the details from Collab Table
                var collabs = this.fundooContext.Collab.ToList();
                if (collabs != null)
                {
                    return collabs;
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
