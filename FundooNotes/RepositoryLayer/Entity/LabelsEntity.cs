// <copyright file="LabelsEntity.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    ///   Labels entity class
    /// </summary>
    public class LabelsEntity
    {
        /// <summary>Gets or sets the label identifier.</summary>
        /// <value>The label identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }

        /// <summary>Gets or sets the name of the label.</summary>
        /// <value>The name of the label.</value>
        public string LabelName { get; set; }

        /// <summary>Gets or sets the note identifier.</summary>
        /// <value>The note identifier.</value>
        [ForeignKey("notes")]
        public long NoteId { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ForeignKey("user")]
        public long Id { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public virtual UserEntity user { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        public virtual NotesEntity notes { get; set; }
    }
}
