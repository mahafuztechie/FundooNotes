//-----------------------------------------------------------------------
// <copyright file="UserBL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//--
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// business layer User class
    /// </summary>
    public class UserBL : IUserBL
    {
        /// <summary>
        /// object reference for interface user repository layer
        /// </summary>
        private readonly IUserRL userRL;

        /// <summary>
        /// constructor with dependency injection
        /// </summary>
        /// <param name="userRL">user repository layer</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>user</returns>
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                return this.userRL.Registration(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="userLogin">user login</param>
        /// <returns>user JWT token</returns>
        public string Login(UserLogin userLogin)
        {
            try
            {
                return this.userRL.Login(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// forget password
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>string </returns>
        public string ForgetPassword(string email)
        {
            try
            {
                return this.userRL.ForgetPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <param name="confirmPassword">confirm password</param>
        /// <returns>bool value</returns>
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
