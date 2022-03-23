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
                    return this.Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
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
                    return this.Ok(new { Success = true, message = "Login Successful", data = user });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email and Password" });
                }
            }
            catch (Exception)
            {
                throw;
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
                    return this.Ok(new { Success = true, message = "mail sent is successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>new reset password string</returns>
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var user = this.userBL.ResetPassword(email, password, confirmPassword);
                if (!user)
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid password" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = "reset password is successful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
