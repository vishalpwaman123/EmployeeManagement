using BusinessModel.Interface;
using BusinessModel.Service;
using CommonModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryModel.Interface;
using RepositoryModel.Service;
using SimpleApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmployeeManagementTestCase
{
    public class UserTestCase
    {
        private IConfiguration configuration;

        UserController userController;
        IUserBL business;
        IUserRL repository;

        public UserTestCase()
        {
            repository = new UserRL();
            business = new UserBL(repository);
            userController = new UserController(business, configuration);
        }

        [Fact]
        public void GivenTestCase_WhenAllValidFields_ShouldReturnOkRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "Vishal";
                UserModel.Lastname = "Wamankar";
                UserModel.EmailId = "rahulkar@gmail.com";
                UserModel.CurrentAddress = "Kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "rahulkar";

                var response = userController.AddUser(UserModel);
                Assert.IsType<OkObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenAllEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "";
                UserModel.Lastname = "";
                UserModel.EmailId = "";
                UserModel.CurrentAddress = "";
                UserModel.Gender = "";
                UserModel.MobileNumber = "";
                UserModel.UserPassword = "";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenAllNullFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = null;
                UserModel.Lastname = null;
                UserModel.EmailId = null;
                UserModel.CurrentAddress = null;
                UserModel.Gender = null;
                UserModel.MobileNumber = null;
                UserModel.UserPassword = null;

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenFirstNameEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "vishalp@gmail.com";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenLastNameEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "";
                UserModel.EmailId = "vishalp@gmail.com";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenEmailIdEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenCurrentAddressEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "vishalp@gmail.com";
                UserModel.CurrentAddress = "";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenGenderEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "vishalp@gmail.com";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenMobileNumberEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "vishalp@gmail.com";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenUserPasswordEmptyFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "vishalp@gmail.com";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        [Fact]
        public void GivenTestCase_WhenSameEmailIdFields_ShouldReturnBadRequest()
        {

            try
            {
                RUserModel UserModel = new RUserModel();
                UserModel.Firstname = "vishal";
                UserModel.Lastname = "waman";
                UserModel.EmailId = "vishal@gmail.com";
                UserModel.CurrentAddress = "kondhwa";
                UserModel.Gender = "male";
                UserModel.MobileNumber = "7758039722";
                UserModel.UserPassword = "vishal@123";

                var response = userController.AddUser(UserModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

    }
}
