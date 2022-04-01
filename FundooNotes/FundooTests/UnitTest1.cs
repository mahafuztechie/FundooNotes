using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using FundooNotes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Sevice;
using System;
using Xunit;

namespace XUnittest
{

    public class UnitTest1
    {
        private readonly IUserBL iuserBL;
        private readonly IUserRL iuserRL;
      
        public static DbContextOptions<FundooContext> newContext { get; }
        public static string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FundooDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        static UnitTest1()
        {
           
            newContext = new DbContextOptionsBuilder<FundooContext>().UseSqlServer(connectionstring).Options;
        }
        public UnitTest1()
        {
           
            var context = new FundooContext(newContext);
            iuserRL = new UserRL(context);
            iuserBL = new UserBL(iuserRL);
        }
        [Fact]
        public void RegisterApi_AddUser_return_OkResult()
        {
            var contoller = new UserController(iuserBL);
            var data = new UserRegistration
            {
                FirstName = "Shawn",
                LastName = "Michael",
                Email = "shawn.3@gmail.com",
                Password = "Shawn&12345"
            };
            var result = contoller.Registration(data);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RegisterApi_AddUser_return_BadResult()
        {
            var contoller = new UserController(iuserBL);
            var data = new UserRegistration
            {
                FirstName = "Shawn",
                LastName = "Michael",
                Email = "shawn.3@gmail.com",
                Password = "Shawn&12345"
            };
            var result = contoller.Registration(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void ResetPasswordApi_AddUser_return_OkResult()
        {
            var contoller = new UserController(iuserBL);
            var data = new ResetPass
            {
                Password = "Shawn@123",
                confirmPassword = "Shawn@123"

            };
            var result = contoller.ResetPassword(data);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ForgetPasswordApi_AddUser_return_OkResult()
        {
            var contoller = new UserController(iuserBL);
            string email = "janto4115@gmail.com";
            var result = contoller.ForgetPassword(email);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void LoginApi_return_OkResult()
        {
            var contoller = new UserController(iuserBL);
            var data = new UserLogin
            {
                Email = "shawn.3@gmail.com",
                Password = "Shawn@123"
            };
            var result = contoller.Login(data);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}