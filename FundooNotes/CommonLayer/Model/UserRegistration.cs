// <copyright file="UserRegistration.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// User registration model
    /// </summary>
    public class UserRegistration
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]{2,}$", ErrorMessage = "Enter a valid first name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]{2,}$", ErrorMessage = "Enter a valid last name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [RegularExpression("^[a-zA-z]{3}([+-_.]*[a-zA-Z0-9]+)*[@][a-zA-z0-9]+(.[a-z]{2,3})*$", ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&-+=()]).{8,}$", ErrorMessage = "Enter a valid email password")]
        public string Password { get; set; }
    }
}
