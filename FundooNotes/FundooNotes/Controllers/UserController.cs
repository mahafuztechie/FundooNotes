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
    using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;
        string sessionName = "fullName";
        string sessionEmail = "email";

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
                HttpContext.Session.SetString(sessionName, user.FirstName);
                HttpContext.Session.SetString(sessionEmail, user.Email);


                var result = this.userBL.Registration(user);
                if (result != null)
                {
                    string sName = HttpContext.Session.GetString(sessionName);
                    string sEmail = HttpContext.Session.GetString(sessionEmail);


                    // _logger.LogInformation("registeration successful");
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "New User Added Successful", Data = "Session || Name : " + sName + "|| Email Id : " + sEmail });
                }
                else
                {
                   // _logger.LogError("registration unsucessful");
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Registration UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.ToString());
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
                   // _logger.LogInformation("login successfull");
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "Login Successful", Data = user });
                }
                else
                {
                    //_logger.LogError("login unsucessful");
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Enter Valid Email and Password" });
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.ToString());
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
                   // _logger.LogInformation("forgot password verification email sent");
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "mail sent is successful" });
                }
                else
                {
                   // _logger.LogError("email is not valid");
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.ToString());
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
                //var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                string  email = "shawn.3@gmail.com";
                var user = this.userBL.ResetPassword(resetPass, email);
                if (!user)
                {
                   // _logger.LogError("enter valid password");
                    return this.BadRequest(new ExceptionModel<string>() { Status = false, Message = "Enter Valid password" });
                }
                else
                {
                   // _logger.LogInformation("reset password successful");
                    return this.Ok(new ExceptionModel<string>() { Status = true, Message = "reset password is successful" });
                }
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex.ToString());
                return this.NotFound(new ExceptionModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
