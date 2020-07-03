using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessModel.Interface;
using CommonModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApplication.Controllers
{
    /// <summary>
    /// Define class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        /// <summary>
        /// define business registration interface object
        /// </summary>
        private readonly BusinessRegistrationInterface employeeBusiness;

        /// <summary>
        /// Declaration of constructor
        /// </summary>
        /// <param name="employeeBusiness"></param>
        public RegistrationController(BusinessRegistrationInterface employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        /// <summary>
        /// Declaration of add employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>Return action</returns>
        [HttpPost]
        [Route("RegistrationEmployee")]
        public async Task<IActionResult> AddEmployeeData(RegistrationModel employeeModel)
        {
            try
            {
                var responseMessage = await this.employeeBusiness.AddEmployeeData(employeeModel);
                if (responseMessage != null)
                {
                    var status = "RegistrationController SuccessFull";
                    return this.Ok(new { status, responseMessage });
                }
                else
                {
                    var status = "RegistrationController Failed";
                    return this.Ok(new { status, responseMessage });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Declaration of employee login method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return action result</returns>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> EmployeeLogin(RegistrationModel employeeModel)
        {
            try
            {
                var responseMessage =  this.employeeBusiness.EmployeeLogin(employeeModel);

                if (responseMessage != null)
                {
                    var status = "User Login SuccessFull";
                    return this.Ok(new { status });
                }
                else
                {
                    var status = "User Login Failed";
                    return this.Ok(new { status });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
