// <copyright file="NotesEntity.cs" company="mahafuz">
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
    ///   notes entity class
    /// </summary>
    public class NotesEntity
    {
        /// <summary>Gets or sets the notes identifier.</summary>
        /// <value>The notes identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the reminder.</summary>
        /// <value>The reminder.</value>
        public DateTime Reminder { get; set; }

        /// <summary>Gets or sets the color.</summary>
        /// <value>The color.</value>
        public string Color { get; set; }

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        public string Image { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is trash.</summary>
        /// <value>
        /// <c>true</c> if this instance is trash; otherwise, <c>false</c>.</value>
        public bool IsTrash { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is archive.</summary>
        /// <value>
        /// <c>true</c> if this instance is archive; otherwise, <c>false</c>.</value>
        public bool IsArchive { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is pinned.</summary>
        /// <value>
        /// <c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        public bool IsPinned { get; set; }

        /// <summary>Gets or sets the created at.</summary>
        /// <value>The created at.</value>
        public DateTime? createdAt { get; set; }

        /// <summary>Gets or sets the modified at.</summary>
        /// <value>The modified at.</value>
        public DateTime? modifiedAt { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ForeignKey("user")]
        public long Id { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public UserEntity user { get; set; }
    }
}
