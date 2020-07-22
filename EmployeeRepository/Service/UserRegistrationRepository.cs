﻿//-------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Service
{
    using CommonModel.Models;
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
    /// Define employee registration repository class
    /// </summary>
    public class UserRegistrationRepository : RepositoryRegistrationInterface
    {
        private SqlConnection sqlConnectionVariable;

        public UserRegistrationRepository()
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
        /// Declaration of add employee data method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return boolean value</returns>
        public RegistrationModel AddEmployeeData(RegistrationModel employeeModel)
        {
            try              
            {
                IList<RegistrationModel> employeeModelsList = new List<RegistrationModel>();
                employeeModel.UserPassword=Encrypt(employeeModel.UserPassword).ToString();
                SqlCommand sqlCommand = new SqlCommand("spAddRegistrationData", this.sqlConnectionVariable);
                SqlCommand sqlCommand1 = new SqlCommand("spGetUserTableData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                
                sqlCommand.Parameters.AddWithValue("@Firstname", employeeModel.Firstname);
                sqlCommand.Parameters.AddWithValue("@Lastname", employeeModel.Lastname);
                sqlCommand.Parameters.AddWithValue("@EmailID", employeeModel.EmailId);
                sqlCommand.Parameters.AddWithValue("@UserPassword", employeeModel.UserPassword);
                sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.MobileNumber);
                sqlCommand.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                
                this.sqlConnectionVariable.Open();
                
                var response =  sqlCommand.ExecuteNonQuery();

                this.sqlConnectionVariable.Close();
                if (response == -1)
                {   
                    
                    return GetSpecificEmployeeAllDetailes(employeeModel.EmailId);
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
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        public IList<RegistrationModel> EmployeeLogin(UserMode employeeModel)
        {
            try
            {
                IList<RegistrationModel> employeeModelsList = new List<RegistrationModel>();
                SqlCommand sqlCommand = new SqlCommand("spFetchLoginData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    RegistrationModel employeeModel1 = new RegistrationModel();
                    /*employeeModel1.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                    employeeModel1.Firstname = sqlDataReader["FirstName"].ToString();
                    employeeModel1.Lastname = sqlDataReader["LastName"].ToString();
                    employeeModel1.CurrentAddress = sqlDataReader["LocalAddress"].ToString();
                    employeeModel1.MobileNumber = sqlDataReader["MobileAddress"].ToString();
                    employeeModel1.Gender = sqlDataReader["Gender"].ToString();*/
                    employeeModel1.EmailId = sqlDataReader["EmailId"].ToString();
                    /*employeeModel1.DayAndTime = sqlDataReader["DayAndTime"].ToString();*/
                    employeeModel1.UserPassword = Decrypt(sqlDataReader["UserPassword"].ToString());
                    
                    if (employeeModel.EmailId == employeeModel1.EmailId )
                    {
                        if (employeeModel.UserPassword == employeeModel1.UserPassword)
                        {
                            employeeModel1.UserPassword = "NULL";
                            employeeModelsList.Add(employeeModel1);
                            FlagsAttribute = 0;
                            break;
                        }
                    }
                }

                this.sqlConnectionVariable.Close();
                if (FlagsAttribute == 0)
                {
                    return employeeModelsList;
                }

                employeeModelsList = null;
                return employeeModelsList;
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

        public RegistrationModel GetSpecificEmployeeAllDetailes(string EmailId)
        {
            try
            {
                RegistrationModel employee = new RegistrationModel();

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

    }
}
