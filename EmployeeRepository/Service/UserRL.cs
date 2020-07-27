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
        private SqlConnection sqlConnectionVariable;

        public UserRL()
        {
            var configuration = this.GetConfiguration();
            this.sqlConnectionVariable = new SqlConnection(configuration.GetSection("Data").GetSection("ConnectionVariable").Value);
        }

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
        /// Define connection variable
        /// </summary>
        //private static readonly string ConnectionVariable = "Server=DESKTOP-OF8D1IH;Database=EmployeeDatabase;Trusted_Connection=true;MultipleActiveResultSets=True";
        
        /// <summary>
        /// Define sql connection variable
        /// </summary>
        //SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionVariable);
        
        /// <summary>
        /// Declaration of add UserInstance data method
        /// </summary>
        /// <param name="employeeModel">Passing UserInstance model object</param>
        /// <returns>return boolean value</returns>
        public UserModel AddEmployeeData(RUserModel UserModel)
        {
            try              
            {
                if (EmailChecking(UserModel.EmailId))
                {
                    IList<UserModel> employeeModelsList = new List<UserModel>();
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
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        public UserModel UserLogin(UserMode employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spFetchLoginData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                UserModel employeeModel1 = new UserModel();
                while (sqlDataReader.Read())
                {
                    
                    employeeModel1.EmailId = sqlDataReader["EmailId"].ToString();
                    employeeModel1.UserPassword = Decrypt(sqlDataReader["UserPassword"].ToString());
                    
                    if (employeeModel.EmailId == employeeModel1.EmailId )
                    {
                        if (employeeModel.UserPassword == employeeModel1.UserPassword)
                        {
                            employeeModel1.UserPassword = "NULL";
                            employeeModel1.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                            employeeModel1.Firstname = sqlDataReader["FirstName"].ToString();
                            employeeModel1.Lastname = sqlDataReader["LastName"].ToString();
                            employeeModel1.CurrentAddress = sqlDataReader["LocalAddress"].ToString();
                            employeeModel1.MobileNumber = sqlDataReader["MobileAddress"].ToString();
                            employeeModel1.Gender = sqlDataReader["Gender"].ToString();
                            employeeModel1.DayAndTime = sqlDataReader["DayAndTime"].ToString();
                            FlagsAttribute = 0;
                            break;
                        }
                    }
                }

                this.sqlConnectionVariable.Close();
                if (FlagsAttribute == 0)
                {
                    return employeeModel1;
                }

                employeeModel1 = null;
                return employeeModel1;
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

        public UserModel GetSpecificEmployeeAllDetailes(string EmailId)
        {
            try
            {
                UserModel employee = new UserModel();

                SqlCommand sqlCommand = new SqlCommand("spGetAllUserData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {

                    employee.EmailId = sqlDataReader["EmailId"].ToString();
                    if (EmailId == employee.EmailId)
                    {
                        employee.Firstname = sqlDataReader["FirstName"].ToString();
                        employee.Lastname = sqlDataReader["LastName"].ToString();
                        employee.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                        employee.CurrentAddress = sqlDataReader["LocalAddress"].ToString();
                        employee.MobileNumber = sqlDataReader["MobileAddress"].ToString();
                        employee.Gender = sqlDataReader["Gender"].ToString();
                        employee.DayAndTime = sqlDataReader["DayAndTime"].ToString();
                        break;
                    }
                }
                this.sqlConnectionVariable.Close();
                return employee;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public bool EmailChecking(string gmailId)
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
                if (EmailId == gmailId)
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
