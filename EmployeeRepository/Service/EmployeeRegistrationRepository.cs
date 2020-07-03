//-------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Service
{
    using CommonModel.Models;
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
    public class EmployeeRegistrationRepository : RepositoryRegistrationInterface
    {
        /// <summary>
        /// Define flag attribute 
        /// </summary>
        private int FlagsAttribute=1;

        /// <summary>
        /// Define connection variable
        /// </summary>
        private static readonly string ConnectionVariable = "Server=DESKTOP-OF8D1IH;Database=EmployeeDatabase;Trusted_Connection=true;MultipleActiveResultSets=True";
        
        /// <summary>
        /// Define sql connection variable
        /// </summary>
        SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionVariable);
        
        /// <summary>
        /// Declaration of add employee data method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return boolean value</returns>
        public async Task<bool> AddEmployeeData(RegistrationModel employeeModel)
        {
            try
            {
                employeeModel.UserPassword=Encrypt(employeeModel.UserPassword).ToString();
                SqlCommand sqlCommand = new SqlCommand("spAddRegistrationData", this.sqlConnectionVariable);
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
                var response = await sqlCommand.ExecuteNonQueryAsync();
                if (response > 0)
                {
                    return true;
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
        /// <param name="employeeModel">Passing registration model object</param>
        /// <returns>Return list</returns>
        public IList<RegistrationModel> EmployeeLogin(RegistrationModel employeeModel)
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
                    employeeModel1.Firstname = sqlDataReader["FirstName"].ToString();
                    employeeModel1.EmailId = sqlDataReader["EmailId"].ToString();
                    employeeModel1.UserPassword = Decrypt(sqlDataReader["UserPassword"].ToString());
                    if (employeeModel.Firstname == employeeModel1.Firstname || employeeModel.EmailId == employeeModel1.EmailId )
                    {
                        if (employeeModel.UserPassword == employeeModel1.UserPassword)
                        {
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
    }
}
