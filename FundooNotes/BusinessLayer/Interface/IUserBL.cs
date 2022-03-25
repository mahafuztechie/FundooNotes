//-----------------------------------------------------------------------
// <copyright file="IUserBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//---------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
   
    /// <summary>
    /// business layer User interface
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// registration method for user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>user entity</returns>
        public UserEntity Registration(UserRegistration user);

        /// <summary>
        /// login method
        /// </summary>
        /// <param name="userLogin">user login </param>
        /// <returns>string value</returns>
        public string Login(UserLogin userLogin);

        /// <summary>
        /// forget password method
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>string value</returns>
        public string ForgetPassword(string email);

        /// <summary>
        /// reset pass method
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <param name="confirmPassword">confirm password</param>
        /// <returns>boolean value</returns>
        public bool ResetPassword(ResetPass resetPass, string email );
    }
}
