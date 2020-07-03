using BusinessModel.Interface;
using BusinessModel.Service;
using EmployeeApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using RepositoryModel.Interface;
using RepositoryModel.Service;
using System;
using Xunit;

namespace WebApiTestCase
{
    public class UnitTestCase
    {
        EmployeesController employeeController;
        BusinessInterface business;
        RepositoryInterface repository;
        public UnitTestCase()
        {
            repository = new EmployeesRepository();
            business = new EmployeeBusiness(repository);
            employeeController = new EmployeesController(business);
        }

        [Fact]
        public void GivenTestCase_WhenGetResponse_ShouldReturnOKStatement()
        {
            var OkResult = employeeController.GetAllEmployee();
            Assert.IsType<OkObjectResult>(OkResult);
        }

        [Fact]
        public void GivenTestCase_WhenPassNull_ShouldReturnOKStatement()
        {
            var OkResult = employeeController.GetOneEmployee(null);
            Assert.IsType<OkObjectResult>(OkResult);
        }
    }
}
