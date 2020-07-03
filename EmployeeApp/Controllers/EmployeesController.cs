//-------------------------------------------------------------------------
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
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Define Class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Define business interface object
        /// </summary>
        private readonly BusinessInterface employeeBusiness;

        /// <summary>
        /// Define Constructor
        /// </summary>
        /// <param name="employeeBusiness"></param>
        public EmployeesController(BusinessInterface employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

      
        /// <summary>
        /// Define add employee data method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return action result</returns>
        //[Route("addEmployeeData")]
        [HttpPost]
        public async Task<IActionResult> AddEmployeeData(EmployeeModel employeeModel)
        {
            try
            {
                int responseMessage = await this.employeeBusiness.AddEmployeeData(employeeModel);
                if (responseMessage == 1 )
                {
                    var status = "Success";
                    var Message = "Add Employee Data Sucessfully ";
                    return this.Ok(new { status , Message , Data = responseMessage });
                }
                else if (responseMessage == 0)
                {
                    var status = "Failed";
                    var Message = " Employee Insertion Failed ";
                    return this.Ok(new { status , Message , Data = responseMessage });
                }
                else
                {
                    var status = "Email Insertion Failed";
                    var Message = " Employee Insertion Failed , Employee Email Must Be Unique ";
                    return this.Ok(new { status , Message , Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// declaration of get all employee method
        /// </summary>
        /// <returns>return action result</returns>
        [HttpGet]
        //[Route("getAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var responseMessage = this.employeeBusiness.GetAllEmployee();
                var status = "Success";
                var Message = " Employee Data Found Sucessfully ";
                return this.Ok(new { status, Message ,responseMessage });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// declaration delete employee method
        /// </summary>
        /// <param name="employeeModel">passing empid</param>
        /// <returns>return action result</returns>
        [HttpDelete("{EmpId}")]
        //[Route("deleteEmployeeData")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] EmployeeModel employeeModel)
        {
            try
            {
                int responseMessage = await this.employeeBusiness.DeleteEmployee(employeeModel);
                //return this.Ok(new { response });

                if (responseMessage == 1)
                {
                    var status = "Success";
                    var Message = "Delete Employee Data Sucessfully ";
                    return this.Ok(new { status, Message, Data = responseMessage });
                }
                else 
                {
                    var status = "Failed";
                    var Message = "Delete Employee Data Failed ";
                    return this.Ok(new { status, Message, Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// declaration update employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return action</returns>
        [HttpPost]
        [Route("updateEmployeeData")]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var responseMessage = await this.employeeBusiness.UpdateEmployee(employeeModel);
                //return this.Ok(new { response });
                if (responseMessage == true)
                {
                    var status = "Success";
                    var Message = " Employee Data Sucessfully Update";
                    return this.Ok(new { status, Message, Data = responseMessage });
                }
                else
                {
                    var status = "Failed";
                    var Message = " Employee Data Updation Failed ";
                    return this.Ok(new { status, Message, Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// declaration of get one employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return action result</returns>
        [HttpGet]
        [Route("SearchOneEmployee")]
        public IActionResult GetOneEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var responseMessage = this.employeeBusiness.SearchOneEmployee(employeeModel);
                var status = "Success";
                var Message = " Employee Data Found Sucessfully";
                return this.Ok(new { status, Message , Data =responseMessage });
        
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Declaration of get specific employee detail method
        /// </summary>
        /// <param name="Id">passing id</param>
        /// <returns>return action result</returns>
        [HttpGet("{Id}")]
        public ActionResult GetSpecificEmployeeDetails([FromRoute] int Id)
        {
            try
            {
                var result = employeeBusiness.GetSpecificEmployeeDetails(Id);
                //if result is not equal to zero then details found
                if (!result.Equals(null))
                {
                    var Status = "Success";
                    var Message = "Employee Data found ";
                    return this.Ok(new { Status, Message, Data = result });
                }
                else
                {
                    var Status = "Unsuccess";
                    var Message = "Employee Data is Not found";
                    return this.BadRequest(new { Status, Message, Data = result });
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
