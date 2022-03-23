//-----------------------------------------------------------------------
// <copyright file="LabelsBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//----------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;
   
    /// <summary>
    /// Business layer Label class
    /// </summary>
    public class LabelsBL : ILabelsBL
    {
        /// <summary>
        /// object refence of label RL
        /// </summary>
        ILabelsRL labelsRL;

        /// <summary>
        /// consturtor wit dependecy injection
        /// </summary>
        /// <param name="labelsRL"></param>
        public LabelsBL(ILabelsRL labelsRL)
        {
            this.labelsRL = labelsRL;
        }

        /// <summary>
        /// create a label
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="noteID">note id</param>
        /// <param name="labelName">label name</param>
        /// <returns></returns>
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
                return this.labelsRL.CreateLabel(userID, noteID, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// fetch all labels
        /// </summary>
        /// <returns>all labels</returns>
        public IEnumerable<LabelsEntity> GetAllLabels()
        {
            try
            {
                return this.labelsRL.GetAllLabels();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get labels by note id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <returns>all labels matchingnote id</returns>
        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                return this.labelsRL.GetLabelsByNoteID(userID, noteID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// remove a label by label name
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="labelName"></param>
        /// <returns>return a bool value</returns>
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                return this.labelsRL.RemoveLabel(userID, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// remove a label by note id
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="noteID">note id</param>
        /// <param name="labelName">label name</param>
        /// <returns>return bool value</returns>
        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName)
        {
            try
            {
                return this.labelsRL.RemoveLabelByNoteID(userID, noteID, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// update the label
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="oldLabelName">old label name</param>
        /// <param name="labelName">new label name</param>
        /// <param name="noteId">note id</param>
        /// <returns>labels Entity</returns>
        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName, long noteId)
        {
            try
            {
                return this.labelsRL.RenameLabel(userID, oldLabelName, labelName, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
