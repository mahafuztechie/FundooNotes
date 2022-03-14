using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;

        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration user)
        {
            try
            {
                var result = userBL.Registration(user);
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

        [HttpPost("forgotPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var user = this.userBL.ForgetPassword(email);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "mail sent is successful"});
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
