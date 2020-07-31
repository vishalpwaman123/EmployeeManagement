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
    using CommonModel.ResponseModels;
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
        UserResponseModel AddEmployeeData(RUserModel employeeModel);

        /// <summary>
        /// Define employee login method
        /// </summary>
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        UserModel UserLogin(UserMode employeeModel);

        /// <summary>
        /// Define get special employee all detailes method
        /// </summary>
        /// <param name="EmailId">Passing email sting</param>
        /// <returns>return user model</returns>
        UserResponseModel GetSpecificEmployeeAllDetailes(string EmailId);

        /// <summary>
        /// Define forget password method
        /// </summary>
        /// <param name="forgetpasswordModel">Passing forget password model object</param>
        /// <returns>return boolean value</returns>
        bool ForgetPassword(ForgetPasswordModel forgetpasswordModel);

        /// <summary>
        /// Define reset password method
        /// </summary>
        /// <param name="resetPasswordModel">passing reset password model object</param>
        /// <param name="EmailId">passing email id string</param>
        /// <returns>return boolean value</returns>
        bool ResetPassword(ResetPasswordModel resetPasswordModel, string EmailId);

    }
}
