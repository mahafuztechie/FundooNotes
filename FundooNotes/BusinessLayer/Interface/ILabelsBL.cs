//-----------------------------------------------------------------------
// <copyright file="ILabelsBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//---------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RepositoryLayer.Entity;
    
    /// <summary>
    /// business layer label interface
    /// </summary>
    public interface ILabelsBL
    {
        /// <summary>
        /// create a label
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="noteID">notes id</param>
        /// <param name="labelName">label name</param>
        /// <returns>label entity</returns>
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName);

        /// <summary>
        /// update a label name
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="oldLabelName">old label name</param>
        /// <param name="labelName">new label name</param>
        /// <param name="noteId">note id</param>
        /// <returns>label entity</returns>
        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName, long noteId);

        /// <summary>
        /// remove label
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="labelName">label name</param>
        /// <returns>boolean value</returns>
        public bool RemoveLabel(long userID, string labelName);

        /// <summary>
        /// remove label for single note 
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="noteID">note id</param>
        /// <param name="labelName">label name</param>
        /// <returns>boolean value</returns>
        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName);

        /// <summary>
        /// fetch list of label entity
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="noteID">note id</param>
        /// <returns>list of label entity</returns>
        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID);

        /// <summary>
        /// fetch all labels from database
        /// </summary>
        /// <returns>all label entities from database</returns>
        public IEnumerable<LabelsEntity> GetAllLabels();
    }
}
