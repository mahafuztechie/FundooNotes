// <copyright file="FundooContext.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using RepositoryLayer.Entity;

    /// <summary>
    ///   context class
    /// </summary>
    public class FundooContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="FundooContext" /> class.</summary>
        /// <param name="options">The options for this context.</param>
        public FundooContext(DbContextOptions options)
           : base(options)
        {
        }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public DbSet<UserEntity> User { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        public DbSet<NotesEntity> Notes { get; set; }

        /// <summary>Gets or sets the collaborator.</summary>
        /// <value>The collaborator.</value>
        public DbSet<CollaboratorEntity> Collab { get; set; }

        /// <summary>Gets or sets the labels.</summary>
        /// <value>The labels.</value>
        public DbSet<LabelsEntity> Labels { get; set; }
    }
}
