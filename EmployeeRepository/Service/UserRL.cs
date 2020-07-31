//-------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Service
{
    using CommonModel.Models;
    using CommonModel.RequestModels;
    using CommonModel.ResponseModels;
    using Microsoft.Extensions.Configuration;
    using RepositoryModel.Interface;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define UserInstance registration repository class
    /// </summary>
    public class UserRL : IUserRL
    {
        /// <summary>
        /// Database Connection Variable
        /// </summary>
        private SqlConnection sqlConnectionVariable;

        public UserRL()
        {
            var configuration = this.GetConfiguration();
            this.sqlConnectionVariable = new SqlConnection(configuration.GetSection("Data").GetSection("ConnectionVariable").Value);
        }

        /// <summary>
        /// Define information configuration method
        /// </summary>
        /// <returns>return builder</returns>
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        /// <summary>
        /// Define flag attribute 
        /// </summary>
        private int FlagsAttribute=1;

        /// <summary>
        /// Declaration of add UserInstance data method
        /// </summary>
        /// <param name="employeeModel">Passing UserInstance model object</param>
        /// <returns>return boolean value</returns>
        public UserResponseModel AddEmployeeData(RUserModel UserModel)
        {
            try              
            {
                if (EmailChecking(UserModel.EmailId))
                {
    
                    UserModel.UserPassword = Encrypt(UserModel.UserPassword).ToString();
                    SqlCommand sqlCommand = new SqlCommand("spAddRegistrationData", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Firstname", UserModel.Firstname);
                    sqlCommand.Parameters.AddWithValue("@Lastname", UserModel.Lastname);
                    sqlCommand.Parameters.AddWithValue("@EmailID", UserModel.EmailId);
                    sqlCommand.Parameters.AddWithValue("@UserPassword", UserModel.UserPassword);
                    sqlCommand.Parameters.AddWithValue("@CurrentAddress", UserModel.CurrentAddress);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", UserModel.MobileNumber);
                    sqlCommand.Parameters.AddWithValue("@Gender", UserModel.Gender);

                    this.sqlConnectionVariable.Open();

                    var response = sqlCommand.ExecuteNonQuery();

                    this.sqlConnectionVariable.Close();
                    if (response == -1)
                    {

                        return GetSpecificEmployeeAllDetailes(UserModel.EmailId);
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        /// <summary>
        /// Declaration UserInstance login method
        /// </summary>
        /// <param name="UserModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        public UserModel UserLogin(UserMode UserModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spFetchLoginData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                UserModel UserModels = new UserModel();
                while (sqlDataReader.Read())
                {
                    
                    UserModels.EmailId = sqlDataReader["EmailId"].ToString();
                    string UserPassword = Decrypt(sqlDataReader["UserPassword"].ToString());
                    
                    if (UserModel.EmailId == UserModels.EmailId )
                    {
                        if (UserModel.UserPassword == UserPassword)
                        {
                            UserModels.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                            UserModels.Firstname = sqlDataReader["FirstName"].ToString();
                            UserModels.Lastname = sqlDataReader["LastName"].ToString();
                            UserModels.CurrentAddress = sqlDataReader["LocalAddress"].ToString();
                            UserModels.MobileNumber = sqlDataReader["MobileAddress"].ToString();
                            UserModels.Gender = sqlDataReader["Gender"].ToString();
                            UserModels.DayAndTime = sqlDataReader["DayAndTime"].ToString();
                            FlagsAttribute = 0;
                            break;
                        }
                    }
                }

                this.sqlConnectionVariable.Close();
                if (FlagsAttribute == 0)
                {
                    return UserModels;
                }

                UserModels = null;
                return UserModels;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration of encrypt method
        /// </summary>
        /// <param name="originalString">Passing password string</param>
        /// <returns>return string</returns>
        public static string Encrypt(string originalString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException ("The string which needs to be encrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes),
                CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Declaration of decrypt method
        /// </summary>
        /// <param name="encryptedString">passing string</param>
        /// <returns>return string</returns>
        public static string Decrypt(string encryptedString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes),
                CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
        /// <summary>
        /// Declare get specific UserData all detailes method
        /// </summary>
        /// <param name="EmailId">Passing email id string</param>
        /// <returns>return user model object</returns>
        public UserResponseModel GetSpecificEmployeeAllDetailes(string EmailId)
        {
            try
            {
                UserResponseModel UserData = new UserResponseModel();

                SqlCommand sqlCommand = new SqlCommand("spGetAllUserData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {

                    UserData.EmailId = sqlDataReader["EmailId"].ToString();
                    if (EmailId == UserData.EmailId)
                    {
                        UserData.Firstname = sqlDataReader["FirstName"].ToString();
                        UserData.Lastname = sqlDataReader["LastName"].ToString();
                        UserData.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                        UserData.CurrentAddress = sqlDataReader["LocalAddress"].ToString();
                        UserData.MobileNumber = sqlDataReader["MobileAddress"].ToString();
                        UserData.Gender = sqlDataReader["Gender"].ToString();
                        UserData.DayAndTime = sqlDataReader["DayAndTime"].ToString();
                        break;
                    }
                }
                this.sqlConnectionVariable.Close();
                return UserData;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Declare email checking method
        /// </summary>
        /// <param name="emailId">Passing email id string</param>
        /// <returns>return boolean value</returns>
        public bool EmailChecking(string emailId)
        {
            string EmailId;
            SqlCommand sqlCommand = new SqlCommand("spcheckemailId", this.sqlConnectionVariable);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Flag", 0);
            this.sqlConnectionVariable.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                EmailId = sqlDataReader["EmailId"].ToString();
                if (EmailId == emailId)
                {
                    return false;
                }
            }
            this.sqlConnectionVariable.Close();
            return true;
        }

        public bool ForgetPassword(ForgetPasswordModel forgetpasswordModel)
        {
            try
            {
                int Flag;
                ForgetPasswordModel UserInstance = new ForgetPasswordModel();
                SqlCommand sqlCommand = new SqlCommand("spgetEmailByCondition", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmailId", forgetpasswordModel.EmailId);
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                Flag = Convert.ToInt32(sqlDataReader["EmpId"]);
                if (Flag > 0)
                {
                   return true;
                }
                this.sqlConnectionVariable.Close();
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Declare reset password method
        /// </summary>
        /// <param name="resetPasswordModel">Passing reset password model object</param>
        /// <param name="EmailId">Passing email id string</param>
        /// <returns>Return boolean value</returns>
        public bool ResetPassword(ResetPasswordModel resetPasswordModel, string EmailId )
        {
            try
            {
                
                    ForgetPasswordModel UserInstance = new ForgetPasswordModel();
                    SqlCommand sqlCommand = new SqlCommand("spResetPassword", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (resetPasswordModel.ConfirmPassword == resetPasswordModel.NewPassword)
                    {
                        resetPasswordModel.NewPassword = Encrypt(resetPasswordModel.NewPassword).ToString();
                        sqlCommand.Parameters.AddWithValue("@EmailId", EmailId);
                        sqlCommand.Parameters.AddWithValue("@NewPassword", resetPasswordModel.NewPassword);
                        this.sqlConnectionVariable.Open();
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                        this.sqlConnectionVariable.Close();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
