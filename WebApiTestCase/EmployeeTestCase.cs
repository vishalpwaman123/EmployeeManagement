

namespace WebApiTestCase
{
    using BusinessModel.Interface;
    using BusinessModel.Service;
    using CommonModel.Exceptions;
    using CommonModel.Models;
    using EmployeeApp.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using RepositoryModel.Interface;
    using RepositoryModel.Service;
    using System;
    using Xunit;

    /// <summary>
    /// Declared employee test case class
    /// </summary>
    public class EmployeeTestCase
    {
        private readonly IDistributedCache distributedCache = null;

        /// <summary>
        /// Define employee controller variable
        /// </summary>
        EmployeesController employeeController;

        /// <summary>
        /// Define employee business variable
        /// </summary>
        IEmployeeBL business;

        /// <summary>
        /// Define employee respository variable
        /// </summary>
        IEmployeeRL repository;

        /// <summary>
        /// Declare employee test case constructor
        /// </summary>
        public EmployeeTestCase()
        {
            repository = new EmployeesRL();
            business = new EmployeeBL(repository);
            employeeController = new EmployeesController(business, distributedCache);
        }

        /*/// <summary>
        /// Define Given Test Case When All Valid Fields Should Return Ok Request method 
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllValidFields_ShouldReturnOkRequest()
        {
            try
            {
                REmployeeModel EmployeeModel = new REmployeeModel();
                EmployeeModel.Firstname = "Vishal";
                EmployeeModel.Lastname = "Wamankar";
                EmployeeModel.EmailId = "rahulkar@gmail.com";
                EmployeeModel.CurrentAddress = "Kondhwa";
                EmployeeModel.Gender = "male";
                EmployeeModel.mobileNumber = 7758039722; 
                
                var response = employeeController.AddEmployeeData(EmployeeModel);
                Assert.IsType<OkObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Same EmailId Fields Should Return Ok Request method 
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenSameEmailIdFields_ShouldReturnOkRequest()
        {
            try
            {
                REmployeeModel EmployeeModel = new REmployeeModel();
                EmployeeModel.Firstname = "Vishal";
                EmployeeModel.Lastname = "Wamankar";
                EmployeeModel.EmailId = "rahul@gmail.com";
                EmployeeModel.CurrentAddress = "Kondhwa";
                EmployeeModel.Gender = "male";
                EmployeeModel.mobileNumber = 7758039722;

                var response = employeeController.AddEmployeeData(EmployeeModel);
                Assert.IsType<OkObjectResult>(response);
            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }*/

        /*/// <summary>
        /// Define Given Test Case When Empty String Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenEmptyStringFields_ShouldReturnBadRequest()
        {
            try {

                REmployeeModel employeeModel = new REmployeeModel
                {
                    Firstname = "",
                    Lastname = "",
                    EmailId = "",
                    CurrentAddress = "",
                    Gender = "",
                    mobileNumber = 0
                };

                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }catch(CustomeException exception)
            {
                Assert.Equal(CustomeException.ExceptionType.EMPTY_FIELD_EXCEPTION, exception.type);
            }
        }

       *//* /// <summary>
        /// Define Given Test Case When Get All Employee Fields Should Return ok Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllEmployeeFields_ShouldReturnokRequest()
        {
            try
            {
                var response = employeeController.GetAllEmployee();
                Assert.IsType<OkObjectResult>(response);
            }
            catch (CustomeException exception)
            {
                Assert.Equal(CustomeException.ExceptionType.EMPTY_FIELD_EXCEPTION, exception.type);
            }
        }*//*

        /// <summary>
        /// Define Given Test Case When Update Fields Should Return Ok Request method 
        /// </summary>
        *//*[Fact]
        public void GivenTestCase_WhenUpdateFields_ShouldReturnOkRequest()
        {
            try
            {
                REmployeeModel employeeModel = new REmployeeModel
                {
                    Firstname = "rahul",
                    Lastname = "waman",
                    EmailId = "rahul1@gmail.com",
                    CurrentAddress = "kondhwa",
                    Gender = "male",
                    mobileNumber = 9881563158
                };

                var response = employeeController.UpdateEmployee(employeeModel);
                Assert.IsType<OkObjectResult>(response);
               
            }
            catch (CustomeException exception)
            {
                Assert.Equal(CustomeException.ExceptionType.EMPTY_FIELD_EXCEPTION, exception.type);
            }
        }*//*

        /// <summary>
        /// Define Given Test Case When Update Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenUpdateFields_ShouldReturnBadRequest()
        {
            try
            {
                REmployeeModel employeeModel = new REmployeeModel
                {
                    Firstname = "rahul",
                    Lastname = "wamankar",
                    EmailId = "vishal1@gmail.com",
                    CurrentAddress = "kondhwa",
                    Gender = "male",
                    mobileNumber = 9881563158
                };

                var response = employeeController.UpdateEmployee(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (CustomeException exception)
            {
                Assert.Equal(CustomeException.ExceptionType.EMPTY_FIELD_EXCEPTION, exception.type);
            }
        }

        /// <summary>
        /// Define Given Test Case When Null Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenNullFields_ShouldReturnBadRequest()
        {
            try
            {
                REmployeeModel employeeModel = new REmployeeModel
                {
                    Firstname = null,
                    Lastname = null,
                    EmailId = null,
                    CurrentAddress = null,
                    Gender = null,
                    mobileNumber = 0
                };

                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);
            }
            catch (CustomeException exception)
            {
                Assert.Equal(CustomeException.ExceptionType.EMPTY_FIELD_EXCEPTION, exception.type);
            }

        }

        /// <summary>
        /// Define Given Test Case When Invalid First Name Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenInvalidFirstNameFields_ShouldReturnBadRequest()
        {
            try
            {

                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = null;
                employeeModel.Lastname = "Waman";
                employeeModel.EmailId = "rahul@gmail.com";
                employeeModel.CurrentAddress = "kondhwa";
                employeeModel.Gender = "male";
                employeeModel.mobileNumber = 7758039722;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }catch(Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Invalid Last Name Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenInvalidLastNameFields_ShouldReturnBadRequest()
        {
            try
            {
                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = "Vishal";
                employeeModel.Lastname = "";
                employeeModel.EmailId = "rahul@gmail.com";
                employeeModel.CurrentAddress = "kondhwa";
                employeeModel.Gender = "male";
                employeeModel.mobileNumber = 7758039722;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Invalid EmailId Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenInvalidEmailIdFields_ShouldReturnBadRequest()
        {
            try
            {
                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = "Vishal";
                employeeModel.Lastname = "Waman";
                employeeModel.EmailId = "";
                employeeModel.CurrentAddress = "kondhwa";
                employeeModel.Gender = "male";
                employeeModel.mobileNumber = 7758039722;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Invalid Current Address Fields Should Return Bad Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenInvalidCurrentAddressFields_ShouldReturnBadRequest()
        {

            try
            {
                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = "Vishal";
                employeeModel.Lastname = "Waman";
                employeeModel.EmailId = "rahul1@gmail.com";
                employeeModel.CurrentAddress = "";
                employeeModel.Gender = "male";
                employeeModel.mobileNumber = 7758039722;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Invalid Gender Fields Should Return Bad Request Method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenInvalidGenderFields_ShouldReturnBadRequest()
        {
            try
            {
                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = "Vishal";
                employeeModel.Lastname = "Waman";
                employeeModel.EmailId = "rahul1@gmail.com";
                employeeModel.CurrentAddress = "Kondhwa";
                employeeModel.Gender = "";
                employeeModel.mobileNumber = 7758039722;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Invalid Mobile Number Fields Should Return Bad Request Method 
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenInvalidMobileNumberFields_ShouldReturnBadRequest()
        {

            try
            {
                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = "Vishal";
                employeeModel.Lastname = "Waman";
                employeeModel.EmailId = "rahul1@gmail.com";
                employeeModel.CurrentAddress = "Kondhwa";
                employeeModel.Gender = "";
                employeeModel.mobileNumber = 775803972;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }

        /// <summary>
        /// Define Given Test Case When Valid User registration Fields Should Return ok Request method
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenValidUserregistrationFields_ShouldReturnokRequest()
        {

            try
            {
                REmployeeModel employeeModel = new REmployeeModel();
                employeeModel.Firstname = "Vishal";
                employeeModel.Lastname = "Waman";
                employeeModel.EmailId = "rahul1@gmail.com";
                employeeModel.CurrentAddress = "Kondhwa";
                employeeModel.Gender = "";
                employeeModel.mobileNumber = 7758039722;
                var response = employeeController.AddEmployeeData(employeeModel);
                Assert.IsType<BadRequestObjectResult>(response);

            }
            catch (Exception exception)
            {
                Assert.IsType<BadRequestObjectResult>(exception);
            }
        }*/
    }
}
