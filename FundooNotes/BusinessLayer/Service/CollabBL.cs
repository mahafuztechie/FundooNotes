//-----------------------------------------------------------------------
// <copyright file="CollabBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//---------------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;
   
    /// <summary>
    /// business layer Collaborator class 
    /// </summary>
    public class CollabBL : ICollabBL
    {
        /// <summary>
        /// collaborator RL reference object
        /// </summary>
        private readonly ICollabRL collabRL;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        /// <param name="collabRL">Collaborator Repository layer</param>
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        /// <summary>
        /// add a collaborator
        /// </summary>
        /// <param name="collaboratorModel">collaborator Model</param>
        /// <returns>collaborator entity</returns>
        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel)
        {
            try
            {
                return this.collabRL.AddCollaborator(collaboratorModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// fetch a collaborator
        /// </summary>
        /// <returns>collaborator entity</returns>
        public IEnumerable<CollaboratorEntity> GetAllCollab()
        {
            try
            {
                return this.collabRL.GetAllCollab();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// fetch a collaborator by note id
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="userId">user id</param>
        /// <returns>collaborator entity</returns>
        public IEnumerable<CollaboratorEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                return this.collabRL.GetByNoteId(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// remove a collaborator from a note
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="collabId">collaborator Id</param>
        /// <returns>collaborator entity</returns>
        public CollaboratorEntity RemoveCollab(long userId, long collabId)
        {
            try
            {
                return this.collabRL.RemoveCollab(userId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}