using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelsBL : ILabelsBL
    {
        ILabelsRL labelsRL;
        public LabelsBL(ILabelsRL labelsRL)
        {
            this.labelsRL = labelsRL;
        }
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
                return labelsRL.CreateLabel(userID, noteID, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                return labelsRL.GetLabelsByNoteID(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                return labelsRL.RemoveLabel(userID, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName)
        {
            try
            {
                return labelsRL.RemoveLabelByNoteID(userID, noteID, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return labelsRL.RenameLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
