//-------------------------------------------------------------------------
// <copyright file="EmployeeBusinessRegistration.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Service
{
    using BusinessModel.Interface;
    using CommonModel.Models;
    using RepositoryModel.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define class 
    /// </summary>
    public class EmployeeBusinessRegistration : BusinessRegistrationInterface
    {
        /// <summary>
        /// define repository registration interface object
        /// </summary>
        private readonly RepositoryRegistrationInterface employeeRepositoryL;

        /// <summary>
        /// declaration of employee business registration method
        /// </summary>
        /// <param name="employeeRepository">passing repository registration interface object</param>
        public EmployeeBusinessRegistration(RepositoryRegistrationInterface employeeRepository)
        {
            this.employeeRepositoryL = employeeRepository;
        }

        /// <summary>
        /// Declaration add employee data method
        /// </summary>
        /// <param name="employeeModel">passing registration model object</param>
        /// <returns>return bool</returns>
        public async Task<bool> AddEmployeeData(RegistrationModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
                    bool response = await this.employeeRepositoryL.AddEmployeeData(employeeModel);
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
        /// Declaration employee login method
        /// </summary>
        /// <param name="employeeModel">passing registration model object</param>
        /// <returns>return list</returns>
        public IList<RegistrationModel> EmployeeLogin(RegistrationModel employeeModel)
        {
            try
            {
                var response = this.employeeRepositoryL.EmployeeLogin(employeeModel);
                if (response == null)
                {
                    return response;
                }
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
