//-------------------------------------------------------------------------
// <copyright file="UserBL.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Service
{
    using BusinessModel.Interface;
    using CommonModel.Models;
    using CommonModel.RequestModels;
    using RepositoryModel.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define class 
    /// </summary>
    public class UserBL : IUserBL
    {
        /// <summary>
        /// define repository registration interface object
        /// </summary>
        private readonly IUserRL employeeRepositoryL;

        /// <summary>
        /// declaration of employee business registration method
        /// </summary>
        /// <param name="employeeRepository">passing repository registration interface object</param>
        public UserBL(IUserRL employeeRepository)
        {
            this.employeeRepositoryL = employeeRepository;
        }

        /// <summary>
        /// Declaration add employee data method
        /// </summary>
        /// <param name="employeeModel">passing registration model object</param>
        /// <returns>return bool</returns>
        public UserModel AddEmployeeData(RUserModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
                    var response = this.employeeRepositoryL.AddEmployeeData(employeeModel);
                    if ( response != null)
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
        /// Declaration employee login method
        /// </summary>
        /// <param name="employeeModel">passing registration model object</param>
        /// <returns>return list</returns>
        public UserModel UserLogin(UserMode employeeModel)
        {
            try
            {
                var response = this.employeeRepositoryL.UserLogin(employeeModel);
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

        public bool ForgetPassword(ForgetPasswordModel forgetpasswordModel)
        {
            try
            {
                var response = this.employeeRepositoryL.ForgetPassword(forgetpasswordModel);
                if (response == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel, string EmailId)
        {
            try
            {
                var response = this.employeeRepositoryL.ResetPassword(resetPasswordModel, EmailId);
                if (response == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
