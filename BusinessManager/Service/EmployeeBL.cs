//-------------------------------------------------------------------------
// <copyright file="EmployeeBL.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Service
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Metadata;
    using System.Threading.Tasks;
    using BusinessModel.Interface;
    using CommonModel.Exceptions;
    using CommonModel.Models;
    using RepositoryModel.Interface;

        /// <summary>
        /// Define class
        /// </summary>
        public class EmployeeBL : IEmployeeBL
        {
       
        /// <summary>
        /// define repository interface object
        /// </summary>
        private readonly IEmployeeRL employeeRepositoryL;
       
        /// <summary>
        /// define employee business constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public EmployeeBL(IEmployeeRL employeeRepository)
        {
            this.employeeRepositoryL = employeeRepository;
        }

       /// <summary>
       /// Define add employee data declaration
       /// </summary>
       /// <param name="employeeModel">passing employee model object</param>
       /// <returns>return boolean value</returns>
        public EmployeeModel AddEmployeeData(REmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
                    if (employeeModel.Firstname == "" || employeeModel.Lastname == "" || employeeModel.CurrentAddress == "" || employeeModel.EmailId == "" || employeeModel.mobileNumber < 999999999 || employeeModel.Gender == "")
                    {
                        throw new CustomeException(CustomeException.ExceptionType.EMPTY_FIELD_EXCEPTION, "Empty Variable Field");
                    }else if (employeeModel.Firstname == null || employeeModel.Lastname == null || employeeModel.CurrentAddress == null || employeeModel.EmailId == null)
                    {
                        throw new CustomeException(CustomeException.ExceptionType.NULL_FIELD_EXCEPTION, "Empty Variable Field");
                    }

                    var response = this.employeeRepositoryL.AddEmployee(employeeModel);
                    if (response != null)
                    {
                        return response;
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration of get all employee method
        /// </summary>
        /// <returns>return list</returns>
        public IList<EmployeeModel> GetAllEmployee()
        {
            try
            {
                var response = this.employeeRepositoryL.GetAllEmployee();
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration of delete employee method
        /// </summary>
        /// <param name="employee">passing employee model object</param>
        /// <returns>return boolean value</returns>
        public EmployeeModel DeleteEmployee(int EmpId)
        {
            try
            {
                if (EmpId != 0 )
                {
                    var response = this.employeeRepositoryL.DeleteEmployee(EmpId);
                    if (response != null)
                    {
                        return response;
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration of update employee method
        /// </summary>
        /// <param name="employee">passing employee model object</param>
        /// <returns>return boolean value</returns>
        public EmployeeModel UpdateEmployee(REmployeeModel employee)
        {
            try
            {
                if (employee != null)
                {
                    
                    var response =  this.employeeRepositoryL.UpdateEmployee(employee);
                    
                    if (response != null)
                    {
                        return response;
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration of employee detail
        /// </summary>
        /// <param name="Id">passing integer value</param>
        /// <returns>return employee model object</returns>
        public EmployeeModel GetSpecificEmployeeDetails(int Id)
        {
            try
            {
                return employeeRepositoryL.GetSpecificEmployeeDetails(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
