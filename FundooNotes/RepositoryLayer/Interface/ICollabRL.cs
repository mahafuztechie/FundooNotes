// <copyright file="ICollabRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    ///   Collaborator Interface for Repository layer
    /// </summary>
    public interface ICollabRL
    {
        /// <summary>Adds the collaborator.</summary>
        /// <param name="collaboratorModel">The collaborator model.</param>
        /// <returns>
        ///   return a collaborator entity after a collaborator is added
        /// </returns>
        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel);

        /// <summary>Removes the collaborator.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collaborator identifier.</param>
        /// <returns>
        ///   removed collaborator entity is returned
        /// </returns>
        public CollaboratorEntity RemoveCollab(long userId, long collabId);

        /// <summary>Gets the by note identifier.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   list of Collaborator for Note 
        /// </returns>
        public IEnumerable<CollaboratorEntity> GetByNoteId(long noteId, long userId);

        /// <summary>Gets all collaborator.</summary>
        /// <returns>
        ///   list of collaborator from database
        /// </returns>
        public IEnumerable<CollaboratorEntity> GetAllCollab();
    }
}
