//-------------------------------------------------------------------------
// <copyright file="RepositoryRegistrationInterface.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Interface
{
    using CommonModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define Interface
    /// </summary>
    public interface RepositoryRegistrationInterface
    {
        /// <summary>
        /// Define Add employee data method
        /// </summary>
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>return boolean value</returns>
        RegistrationModel AddEmployeeData(RegistrationModel employeeModel);

        /// <summary>
        /// Define employee login method
        /// </summary>
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        IList<RegistrationModel> EmployeeLogin(RegistrationModel employeeModel);

        RegistrationModel GetSpecificEmployeeAllDetailes(string EmailId);
    }
}
