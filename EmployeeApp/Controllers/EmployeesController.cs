﻿//-------------------------------------------------------------------------
// <copyright file="EmployeesController.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace EmployeeApp.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BusinessModel.Interface;
    using CommonModel.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Define Class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private IConfiguration configuration;

        /// <summary>
        /// Define business interface object
        /// </summary>
        private readonly BusinessInterface employeeBusiness;

        /// <summary>
        /// Define Constructor
        /// </summary>
        /// <param name="employeeBusiness"></param>
        public EmployeesController(BusinessInterface employeeBusiness , IConfiguration configuration)
        {
            this.employeeBusiness = employeeBusiness;
            this.configuration = configuration;
        }

        /// <summary>
        /// Define add employee data method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult AddEmployeeData(EmployeeModel employeeModel)
        {
            try
            {
                var responseMessage =  this.employeeBusiness.AddEmployeeData(employeeModel);
                if (responseMessage != null )
                {
                    bool Success = true;
                    var Message = "Add Employee Data Sucessfully ";
                    return this.Ok(new { Success , Message , Data = responseMessage });
                }
                else 
                {
                    bool Success = false;
                    var Message = " Employee Insertion Failed ";
                    return this.BadRequest(new { Success , Message , Data = responseMessage });
                }
                
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
        }

        /// <summary>
        /// declaration of get all employee method
        /// </summary>
        /// <returns>return action result</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var responseMessage = this.employeeBusiness.GetAllEmployee();
                bool Success = true;
                var Message = " Employee Data Found Sucessfully ";
                return this.Ok(new { Success, Message ,responseMessage });

            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
        }

        /// <summary>
        /// declaration delete employee method
        /// </summary>
        /// <param name="employeeModel">passing empid</param>
        /// <returns>return action result</returns>
        [HttpDelete("{EmpId}")]
        [Authorize]
        public IActionResult DeleteEmployee([FromRoute] int EmpId)
        {
            try
            {
                //var result = employeeBusiness.GetSpecificEmployeeDetails(employeeModel.EmpId);
                var responseMessage = this.employeeBusiness.DeleteEmployee(EmpId);
                if (responseMessage != null)
                {
                    bool Success = true;
                    var Message = "Delete Employee Data Sucessfully ";
                    return this.Ok(new { Success, Message, Data = responseMessage });
                }
                else 
                {
                    bool Success = false;
                    var Message = "Delete Employee Data Failed ";
                    return this.BadRequest(new { Success, Message, Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
        }

        /// <summary>
        /// declaration update employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return action</returns>
        [HttpPost]
        [Authorize]
        [Route("updateEmployeeData")]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var responseMessage = await this.employeeBusiness.UpdateEmployee(employeeModel);
                //return this.Ok(new { response });
                if (responseMessage == true)
                {
                    bool Success = true;
                    var Message = " Employee Data Sucessfully Update";
                    return this.Ok(new { Success, Message, Data = employeeModel });
                }
                else
                {
                    bool Success = false;
                    var Message = " Employee Data Updation Failed ";
                    return this.BadRequest(new { Success, Message, Data = employeeModel });
                }
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
        }

        

        /// <summary>
        /// Declaration of get specific employee detail method
        /// </summary>
        /// <param name="Id">passing id</param>
        /// <returns>return action result</returns>
        [HttpGet("{Id}")]
        [Authorize]
        public ActionResult GetSpecificEmployeeDetails([FromRoute] int Id)
        {
            try
            {
                var result = employeeBusiness.GetSpecificEmployeeDetails(Id);
                //if result is not equal to zero then details found
                if (!result.Equals(null))
                {
                    bool Success = true;
                    var Message = "Employee Data found ";
                    return this.Ok(new { Success, Message, Data = result });
                }
                else
                {
                    bool Success = false;
                    var Message = "Employee Data is Not found";
                    return this.BadRequest(new { Success , Message, Data = result });
                }

            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success , message = e.Message });
            }
        }
    }
}
