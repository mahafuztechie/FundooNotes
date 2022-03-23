// <copyright file="CollaboratorModel.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// collaborator model class
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public long NoteID { get; set; }

        /// <summary>
        /// Gets or sets the collaborator email.
        /// </summary>
        /// <value>
        /// The collaborator email.
        /// </value>
        public string CollabEmail { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }
    }
}
