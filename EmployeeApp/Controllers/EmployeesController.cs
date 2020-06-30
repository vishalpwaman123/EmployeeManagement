
namespace EmployeeApp.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BusinessModel.Interface;
    using CommonModel.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly BusinessInterface employeeBusiness;

        public EmployeesController(BusinessInterface employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        [Route("ccc")]
        [HttpPost]
        public async Task<IActionResult> AddEmployeeData(EmployeeModel employeeModel)
        {
            try
            {
                var responseMessage = await this.employeeBusiness.AddEmployeeData(employeeModel);
                return this.Ok(new { responseMessage });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpGet]
        [Route("getAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var response = this.employeeBusiness.GetAllEmployee();
                return this.Ok(new { response });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("deleteEmployeeData")]
        public async Task<IActionResult> DeleteEmployee(EmployeeModel employeeModel)
        {
            var response = await this.employeeBusiness.DeleteEmployee(employeeModel);
            return this.Ok(new { response });
        }

        [HttpPost]
        [Route("updateEmployeeData")]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employeeModel)
        {
            var response = await this.employeeBusiness.UpdateEmployee(employeeModel);
            return this.Ok(new { response });
        }

        /*[HttpPost]
        [Route("searchEmployeeData")]
        public IActionResult SearchEmployee(EmployeeModel employeeModel)
        {
            var response = this.employeeBusiness.SearchEmployee(employeeModel);
            return this.Ok(new { response });
        }
        */

        [HttpGet]
        [Route("SearchOneEmployee")]
        public IActionResult GetOneEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var response = this.employeeBusiness.SearchOneEmployee(employeeModel);
                return this.Ok(new { response });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
