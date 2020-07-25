//-------------------------------------------------------------------------
// <copyright file="IUserRL.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Interface
{
    using CommonModel.Models;
    using CommonModel.RequestModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define Interface
    /// </summary>
    public interface IUserRL
    {
        /// <summary>
        /// Define Add employee data method
        /// </summary>
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>return boolean value</returns>
        UserModel AddEmployeeData(RUserModel employeeModel);

        /// <summary>
        /// Define employee login method
        /// </summary>
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        UserModel UserLogin(UserMode employeeModel);

        UserModel GetSpecificEmployeeAllDetailes(string EmailId);

        bool ForgetPassword(ForgetPasswordModel forgetpasswordModel);

        bool ResetPassword(ResetPasswordModel resetPasswordModel, string EmailId);

    }
}
