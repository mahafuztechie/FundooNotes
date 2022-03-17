using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelsBL
    {
        public LabelsEntity CreateLabel(long userID, long noteID, string labelName);
        public IEnumerable<LabelsEntity> RenameLabel(long userID, string oldLabelName, string labelName);
        public bool RemoveLabel(long userID, string labelName);
        public bool RemoveLabelByNoteID(long userID, long noteID, string labelName);
        public IEnumerable<LabelsEntity> GetLabelsByNoteID(long userID, long noteID);
    }
}
