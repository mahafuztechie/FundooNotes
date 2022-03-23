//-----------------------------------------------------------------------
// <copyright file="ICollabBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//---------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    /// Business Layer collaborator interface
    /// </summary>
    public interface ICollabBL
    {
        /// <summary>
        /// add a collaborator for note
        /// </summary>
        /// <param name="collaboratorModel"> email, user & note identifier</param>
        /// <returns>collaborator entity</returns>
        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel);

        /// <summary>
        /// delete a collaborator from note
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="collabId">collaborator identifier</param>
        /// <returns>collaborator entity</returns>
        public CollaboratorEntity RemoveCollab(long userId, long collabId);

        /// <summary>
        /// fetch collaborators by note id
        /// </summary>
        /// <param name="noteId">note identifier</param>
        /// <param name="userId">user identifier</param>
        /// <returns>list of collaborator entity</returns>
        public IEnumerable<CollaboratorEntity> GetByNoteId(long noteId, long userId);

        /// <summary>
        /// fetch all collaborators from database
        /// </summary>
        /// <returns>list of collaborator entity from database</returns>
        public IEnumerable<CollaboratorEntity> GetAllCollab();
    }
}
