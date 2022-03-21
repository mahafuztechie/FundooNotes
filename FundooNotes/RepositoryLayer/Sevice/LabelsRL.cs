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
    public class LabelsRL: ILabelsRL
    {
        FundooContext fundoocontext;
        private readonly IConfiguration configuration;
        public LabelsRL(FundooContext fundoocontext, IConfiguration config)
        {
            this.fundoocontext = fundoocontext;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
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
        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<LabelsEntity> labels;
            labels = fundoocontext.Labels.Where(e => e.Id == userID && e.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = labelName;
                }
                fundoocontext.SaveChanges();
                return labels;
            }
            else
            {
                return null;
            }

        }
        public bool RemoveLabel(long userID, string labelName)
        {
            IEnumerable<LabelsEntity> labels;
            labels = fundoocontext.Labels.Where(e => e.Id == userID && e.LabelName == labelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    fundoocontext.Labels.Remove(label);
                }
                fundoocontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName)
        {
            var label = fundoocontext.Labels.Where(e => e.Id == userID && e.LabelName == labelName && e.NoteId == noteID).FirstOrDefault();
            if (label != null)
            {
                fundoocontext.Labels.Remove(label);
                fundoocontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                var result =fundoocontext.Labels.Where(e => e.NoteId == noteID && e.Id == userID).ToList();
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
