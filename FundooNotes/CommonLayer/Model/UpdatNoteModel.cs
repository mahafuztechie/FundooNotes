// <copyright file="UpdatNoteModel.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Update Note Model class
    /// </summary>
    public class UpdatNoteModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the modified at.
        /// </summary>
        /// <value>
        /// The modified at.
        /// </value>
        public DateTime? modifiedAt { get; set; }
    }
}
