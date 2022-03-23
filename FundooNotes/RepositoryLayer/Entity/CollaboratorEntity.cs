// <copyright file="CollaboratorEntity.cs" company="mahafuz">
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
    ///   Collaborator entity class
    /// </summary>
    public class CollaboratorEntity
    {
        /// <summary>Gets or sets the collaborator identifier.</summary>
        /// <value>The collaborator identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }

        /// <summary>Gets or sets the collaborator email.</summary>
        /// <value>The collaborator email.</value>
        public string CollabEmail { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ForeignKey("user")]
        public long Id { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public UserEntity user { get; set; }

        /// <summary>Gets or sets the notes identifier.</summary>
        /// <value>The notes identifier.</value>
        [ForeignKey("notes")]
        public long NotesId { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        public NotesEntity notes { get; set; }
    }
}
