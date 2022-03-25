// <copyright file="IUserRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    ///   user repository interface
    /// </summary>
    public interface IUserRL
    {
        /// <summary>Registrations the specified user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   User Entity once user is registered
        /// </returns>
        public UserEntity Registration(UserRegistration user);

        /// <summary>Logins the specified user login.</summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>
        ///  token after user  is logged in.
        /// </returns>
        public string Login(UserLogin userLogin);

        /// <summary>Forgets the password.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///  string for forget password
        /// </returns>
        public string ForgetPassword(string email);

        /// <summary>Resets the password.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>
        ///   boolean value true after password is reset successfully
        /// </returns>
        public bool ResetPassword(ResetPass resetPass, string email);
    }
}
