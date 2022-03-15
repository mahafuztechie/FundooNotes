using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UpdatNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? modifiedAt { get; set; }
    }
}
