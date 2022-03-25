// <copyright file="UserController.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer.Entity;

    /// <summary>
    /// user controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The user business layer object reference
        /// </summary>
        private readonly IUserBL userBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The user business layer object reference.</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>registered user data</returns>
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration user)
        {
            try
            {
                var result = this.userBL.Registration(user);
                if (result != null)
                {
                    return this.Ok(new ExceptionModel<UserEntity>() { Status = true, Message = "New User Added Successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Registration UnSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ExceptionModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>string token</returns>
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var user = this.userBL.Login(userLogin);
                if (user != null)
                {
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "Login Successful", Data = user });
                }
                else
                {
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Enter Valid Email and Password" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ExceptionModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>string after forget password verified</returns>
        [HttpPost("forgotPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var user = this.userBL.ForgetPassword(email);
                if (user != null)
                {
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "mail sent is successful" });
                }
                else
                {
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ExceptionModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>new reset password string</returns>
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPass resetPass)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var user = this.userBL.ResetPassword(resetPass, email);
                if (!user)
                {
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Enter Valid password" });
                }
                else
                {
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "reset password is successful" });
                }
            }
            catch (Exception ex)
            {
               return this.NotFound(new ExceptionModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
