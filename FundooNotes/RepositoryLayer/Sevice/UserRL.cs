//-----------------------------------------------------------------------
// <copyright file="UserRL.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
//--
namespace RepositoryLayer.Sevice
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    ///   UserRL class
    /// </summary>
    public class UserRL : IUserRL
    {
        /// <summary>The  object reference</summary>
        private readonly FundooContext fundooContext;

        /// <summary>The configuration object reference</summary>
        private readonly IConfiguration toolSettings;

        /// <summary>Initializes a new instance of the <see cref="UserRL" /> class.</summary>
        /// <param name="fundooContext">The context object reference.</param>
        /// <param name="toolSettings">The tool settings object reference.</param>
        public UserRL(FundooContext fundooContext, IConfiguration toolSettings)
        {
            this.fundooContext = fundooContext;
            this.toolSettings = toolSettings;
        }

        /// <summary>Registrations the specified user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///  Registration User entity
        /// </returns>
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                //register user if does not already exits in database
                var userresult = this.fundooContext.User.FirstOrDefault(u => u.Email == user.Email);
                if (userresult == null)
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    userEntity.Email = user.Email;
                    userEntity.Password = this.PasswordEncrypt(user.Password);
                    this.fundooContext.Add(userEntity);
                    int result = this.fundooContext.SaveChanges();
                    if (result > 0)
                    {
                        return userEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Logins the specified user login.</summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>
        ///   Login string token
        /// </returns>
        public string Login(UserLogin userLogin)
        {
            try
            {
                // if Email and password is empty return null. 
                if (string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
                {
                    return null;
                }

                var result = this.fundooContext.User.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                string dcryptPass = this.PasswordDecrypt(result.Password);
                if (result != null && dcryptPass == userLogin.Password)
                {
                    string token = this.GenerateSecurityToken(result.Email, result.Id);
                    return token;
                }
                else
                {
                    return null;
                }       
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Forgets the password.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   Forget password string
        /// </returns>
        public string ForgetPassword(string email)
        {
            try
            {
                // if password forgotton verfiy user valid through his email by sending token
                var user = this.fundooContext.User.Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    var token = this.GenerateSecurityToken(user.Email, user.Id);
                    new Msmq().Sender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Resets the password.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>
        ///   ResetPassword boolean value
        /// </returns>
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                //reset a new password 
                if (password.Equals(confirmPassword))
                {
                    var user = this.fundooContext.User.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = this.PasswordEncrypt(confirmPassword);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Passwords the encrypt.</summary>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   Password Encrypt string
        /// </returns>
        public string PasswordEncrypt(string password)
        {
            try
            {
                //encrypt a password 
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encryptPass = Convert.ToBase64String(encode);
                return encryptPass;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Passwords the decrypt.</summary>
        /// <param name="encodedPass">The encoded pass.</param>
        /// <returns>
        ///   Password Decrypt string
        /// </returns>
        public string PasswordDecrypt(string encodedPass)
        {
            try
            {
                //decrypt a password
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] toDecodeByte = Convert.FromBase64String(encodedPass);
                int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
                string passDecrypt = new string(decodedChar);
                return passDecrypt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Generates the security token.</summary>
        /// <param name="email">The email.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///    Generate Security a Token for user
        /// </returns>
        private string GenerateSecurityToken(string email, long id)
        {
            //genearte a jwt for authorization
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.toolSettings["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] 
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("Id", id.ToString())
            };
            var token = new JwtSecurityToken(
              this.toolSettings["Jwt:Issuer"],
              this.toolSettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
