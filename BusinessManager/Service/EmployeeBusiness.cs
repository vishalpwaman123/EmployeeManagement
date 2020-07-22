//-------------------------------------------------------------------------
// <copyright file="EmployeeBusiness.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessModel.Interface;
    using CommonModel.Models;
    using RepositoryModel.Interface;

        /// <summary>
        /// Define class
        /// </summary>
        public class EmployeeBusiness : BusinessInterface
        {
       
        /// <summary>
        /// define repository interface object
        /// </summary>
        private readonly RepositoryInterface employeeRepositoryL;
       
        /// <summary>
        /// define employee business constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public EmployeeBusiness(RepositoryInterface employeeRepository)
        {
            this.employeeRepositoryL = employeeRepository;
        }

       /// <summary>
       /// Define add employee data declaration
       /// </summary>
       /// <param name="employeeModel">passing employee model object</param>
       /// <returns>return boolean value</returns>
        public EmployeeModel AddEmployeeData(EmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
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
                if (EmpId != null)
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
        public async Task<bool> UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                if (employee != null)
                {
                    
                    var response = await this.employeeRepositoryL.UpdateEmployee(employee);
                    
                    if (response == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
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
