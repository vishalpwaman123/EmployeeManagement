//-------------------------------------------------------------------------
// <copyright file="BusinessRegistrationInterface.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Interface
{
    using CommonModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define Interface class
    /// </summary>
    public interface BusinessRegistrationInterface
    {   
        /// <summary>
        /// Define add employee data method
        /// </summary>
        /// <param name="employee">passing registration model object</param>
        /// <returns>return boolean value</returns>
        Task<bool> AddEmployeeData(RegistrationModel employee);

        /// <summary>
        /// Define employye login method
        /// </summary>
        /// <param name="employeeModel">passing registration model object</param>
        /// <returns>return list</returns>
        IList<RegistrationModel> EmployeeLogin(RegistrationModel employeeModel);

    }
}
