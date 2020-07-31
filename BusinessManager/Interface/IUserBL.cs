//-------------------------------------------------------------------------
// <copyright file="IUserBL.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Interface
{
    using CommonModel.Models;
    using CommonModel.RequestModels;
    using CommonModel.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define Interface class
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// Define add employee data method
        /// </summary>
        /// <param name="employee">passing registration model object</param>
        /// <returns>return boolean value</returns>
        UserResponseModel AddEmployeeData(RUserModel employee);

        /// <summary>
        /// Define employye login method
        /// </summary>
        /// <param name="employeeModel">passing registration model object</param>
        /// <returns>return list</returns>
        UserModel UserLogin(UserMode employeeModel);

        /// <summary>
        /// Define Forget password method
        /// </summary>
        /// <param name="forgetpasswordModel">passing forget password model object</param>
        /// <returns>return boolean value</returns>
        bool ForgetPassword(ForgetPasswordModel forgetpasswordModel);

        /// <summary>
        /// Define reset password method 
        /// </summary>
        /// <param name="resetPasswordModel">passing reset password model object</param>
        /// <param name="EmailId">Passing email id string</param>
        /// <returns>return boolean value</returns>
        bool ResetPassword(ResetPasswordModel resetPasswordModel , string EmailId);

    }
}
