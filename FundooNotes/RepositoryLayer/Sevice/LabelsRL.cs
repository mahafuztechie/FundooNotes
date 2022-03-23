//-----------------------------------------------------------------------
// <copyright file="LabelsRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//--
namespace RepositoryLayer.Sevice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// LabelsRL class
    /// </summary>
    public class LabelsRL : ILabelsRL
    {
        /// <summary>The  object reference</summary>
        private readonly FundooContext fundoocontext;

        /// <summary>The configuration object reference</summary>
        private readonly IConfiguration configuration;

        /// <summary>Initializes a new instance of the <see cref="LabelsRL" /> class.</summary>
        /// <param name="fundoocontext">The context object reference</param>
        /// <param name="config">The configuration.</param>
        public LabelsRL(FundooContext fundoocontext, IConfiguration config)
        {
            this.fundoocontext = fundoocontext; ////appcontext to for api
            this.configuration = config; ////for startup file instance
        }

        /// <summary>Creates the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///    Create a Label
        /// </returns>
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
                //create a label entity & add to db
                LabelsEntity labelEntity = new LabelsEntity
                {
                    LabelName = labelName,
                    Id = userID,
                    NoteId = noteID
                };
                this.fundoocontext.Labels.Add(labelEntity);
                int result = this.fundoocontext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
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

        /// <summary>Renames the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="oldLabelName">Old name of the label.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///    Rename a Label
        /// </returns>
        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName, long noteId)
        {
            //find if label exist in db & update it
            IEnumerable<LabelsEntity> labels;
            labels = this.fundoocontext.Labels.Where(e => e.Id == userID && e.LabelName == oldLabelName && e.NoteId == noteId).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = labelName;
                }

                this.fundoocontext.SaveChanges();
                return labels;
            }
            else
            {
                return null;
            }
        }

        /// <summary>Removes the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///   Remove a Label
        /// </returns>
        public bool RemoveLabel(long userID, string labelName)
        {
            //remove a label if matches in existing db
            IEnumerable<LabelsEntity> labels;
            labels = this.fundoocontext.Labels.Where(e => e.Id == userID && e.LabelName == labelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    this.fundoocontext.Labels.Remove(label);
                }

                this.fundoocontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Removes the label by note identifier.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///   Remove Label By Note ID
        /// </returns>
        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName)
        {
            //remove a label for matching note id
            var label = this.fundoocontext.Labels.Where(e => e.Id == userID && e.LabelName == labelName && e.NoteId == noteID).FirstOrDefault();
            if (label != null)
            {
                this.fundoocontext.Labels.Remove(label);
                this.fundoocontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Gets the labels by note identifier.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>
        ///   Get Labels By Note ID
        /// </returns>
        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                //fetch all labels matching note id
                var result = this.fundoocontext.Labels.Where(e => e.NoteId == noteID && e.Id == userID).ToList();
                if (result != null)
                {
                    return result;
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

        /// <summary>Gets all labels.</summary>
        /// <returns>
        ///  Get All Labels
        /// </returns>
        public IEnumerable<LabelsEntity> GetAllLabels()
        {
            try
            {
                // Fetch All the details from Labels Table
                var labels = this.fundoocontext.Labels.ToList();
                if (labels != null)
                {
                    return labels;
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
