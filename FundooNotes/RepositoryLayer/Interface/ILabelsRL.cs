// <copyright file="ILabelsRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RepositoryLayer.Entity;

    /// <summary>
    ///   label interface for repository layer
    /// </summary>
    public interface ILabelsRL
    {
        /// <summary>Creates the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///   label after label is created
        /// </returns>
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName);

        /// <summary>Renames the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="oldLabelName">Old name of the label.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   update label for note
        /// </returns>
        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName, long noteId);

        /// <summary>Removes the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///   return true if label is removed
        /// </returns>
        public bool RemoveLabel(long userID, string labelName);

        /// <summary>Removes the label by note identifier.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///   return true if label is removed for note
        /// </returns>
        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName);

        /// <summary>Gets the labels by note identifier.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>
        ///   list of labels matching note id
        /// </returns>
        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID);

        /// <summary>Gets all labels.</summary>
        /// <returns>
        ///   list of labels from database
        /// </returns>
        public IEnumerable<LabelsEntity> GetAllLabels();
    }
}
