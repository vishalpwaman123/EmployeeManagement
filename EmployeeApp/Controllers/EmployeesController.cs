//-------------------------------------------------------------------------
// <copyright file="EmployeesController.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace EmployeeApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessModel.Interface;
    using CommonModel.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    /// <summary>
    /// Define Class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Define business interface object
        /// </summary>
        private readonly IEmployeeBL employeeBusiness;

        /// <summary>
        /// Define Constructor
        /// </summary>
        /// <param name="employeeBusiness"></param>
        public EmployeesController(IEmployeeBL employeeBusiness, IDistributedCache distributedCache)
        {
            this.employeeBusiness = employeeBusiness;
            this.distributedCache = distributedCache;

        }

        /// <summary>
        /// Define add employee data method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult AddEmployeeData(REmployeeModel employeeModel)
        {
            try
            {
                string cacheKey = "employees";

                distributedCache.Remove(cacheKey);

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
                    return this.BadRequest(new { Success , Message });
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
                List<EmployeeModel> employees = null;

                //Variables For Cache.
                string cacheKey = "employees";
                string serializedList;

                var encodedList = distributedCache.Get(cacheKey);

                if (encodedList != null)
                {
                    serializedList = Encoding.UTF8.GetString(encodedList);
                    employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(serializedList);
                }
                else
                {
                    employees = this.employeeBusiness.GetAllEmployee();
                    serializedList = JsonConvert.SerializeObject(employees);
                    encodedList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                                      .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                                      .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedList, options);
                }

                //var responseMessage = this.employeeBusiness.GetAllEmployee();
                if (employees != null)
                {
                    bool Success = true;
                    var Message = " Employee Data Fetch Sucessfully ";
                    return this.Ok(new { Success, Message, data = employees });
                }
                else
                {
                    bool Success = false;
                    var Message = " Failed Fetch Employee Data ";
                    return this.BadRequest(new { Success, Message });
                }
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
                    return this.BadRequest(new { Success, Message });
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
        public IActionResult UpdateEmployee(REmployeeModel employeeModel)
        {
            try
            {
                var responseMessage = this.employeeBusiness.UpdateEmployee(employeeModel);
                //return this.Ok(new { response });
                if (responseMessage != null)
                {
                    bool Success = true;
                    var Message = " Employee Data Sucessfully Update";
                    return this.Ok(new { Success, Message, Data = responseMessage });
                }
                else
                {
                    bool Success = false;
                    var Message = " Employee Data Updation Failed ";
                    return this.BadRequest(new { Success, Message });
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
                EmployeeModel employee;

                string cacheKey = Id.ToString();
                string serializedEmployee;

                //Getting Employee Details From Redis Cache.
                var encodedEmployee = distributedCache.Get(cacheKey);

                //If Redis has employee detail then it will fetch from Redis else it will fetch from Database.
                if (encodedEmployee != null)
                {
                    serializedEmployee = Encoding.UTF8.GetString(encodedEmployee);
                    employee = JsonConvert.DeserializeObject<EmployeeModel>(serializedEmployee);
                }
                else
                {
                    employee = employeeBusiness.GetSpecificEmployeeDetails(Id);
                    serializedEmployee = JsonConvert.SerializeObject(employee);
                    encodedEmployee = Encoding.UTF8.GetBytes(serializedEmployee);
                    var options = new DistributedCacheEntryOptions()
                                     .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                     .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    distributedCache.Set(cacheKey, encodedEmployee, options);
                }
                
                if (!employee.Equals(null))
                {
                    bool Success = true;
                    var Message = "Employee Data is found ";
                    return this.Ok(new { Success, Message, Data = employee });
                }
                else
                {
                    bool Success = false;
                    var Message = "Employee Data is Not found";
                    return this.BadRequest(new { Success , Message });
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
