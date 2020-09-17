//-------------------------------------------------------------------------
// <copyright file="EmployeesRL.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using CommonModel.Models;
    using RepositoryModel.Interface;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    /// <summary>
    /// Define class
    /// </summary>
    public class EmployeesRL : IEmployeeRL
    {
        /// <summary>
        /// Define sql connection variable
        /// </summary>
        private SqlConnection sqlConnectionVariable;

        /// <summary>
        /// Define constructor
        /// </summary>
        public EmployeesRL()
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
        /// declaration of add employee method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return boolean value</returns>
        public EmployeeModel AddEmployee(REmployeeModel employeeModel)
          {
            try
            {
                if (EmailChecking(employeeModel.EmailId))
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddEmployeeData", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Firstname", employeeModel.Firstname);
                    
                    sqlCommand.Parameters.AddWithValue("@Lastname", employeeModel.Lastname);
                    sqlCommand.Parameters.AddWithValue("@EmailID", employeeModel.EmailId);
                    sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.mobileNumber);
                    sqlCommand.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    
                    this.sqlConnectionVariable.Open();
                    //int response =  sqlCommand.ExecuteNonQuery();
                    int status = 1;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        status = sqlDataReader.GetInt32(0);
                        if (status == 1)
                        {
                            this.sqlConnectionVariable.Close();
                            return GetSpecificEmployeeAllDetailes(employeeModel.EmailId);
                        }else
                        {  
                            return null;
                        }
                    }
                    this.sqlConnectionVariable.Close();
                }
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }finally
            {
                this.sqlConnectionVariable.Close();
            }
       
        }

        /// <summary>
        /// Declaration of get all employee method
        /// </summary>
        /// <returns>return list</returns>
        public List<EmployeeModel> GetAllEmployee()
        {
            try
            {
                List<EmployeeModel> employeeModelsList = new List<EmployeeModel>();
                SqlCommand sqlCommand = new SqlCommand("spgetAllEmployee", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    EmployeeModel employeeModel = new EmployeeModel();
                    if (Convert.ToInt32(sqlDataReader["PresentState"]) == 1)
                    {
                        employeeModel.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                        employeeModel.Firstname = sqlDataReader["FirstName"].ToString();
                        employeeModel.Lastname = sqlDataReader["LastName"].ToString();
                        employeeModel.EmailId = sqlDataReader["EmailId"].ToString();
                        employeeModel.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                        employeeModel.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                        employeeModel.Gender = sqlDataReader["Gender"].ToString();
                        employeeModel.DayAndTime = sqlDataReader["ModificationDate"].ToString();
                        employeeModelsList.Add(employeeModel);
                        break;
                    }
                }
                this.sqlConnectionVariable.Close();
                return employeeModelsList;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration delete employee method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>Return boolean value</returns>
        public EmployeeModel DeleteEmployee(int EmpId)
        {
            try
            {
                
                    int status = 1;
                    EmployeeModel employeeModel1 = new EmployeeModel();
                    employeeModel1 = GetSpecificEmployeeDetails(EmpId);
                    SqlCommand sqlCommand = new SqlCommand("spDeleteEmployeeData", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@EmpId", EmpId);
                    this.sqlConnectionVariable.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    status = sqlDataReader.GetInt32(0);
                    if (status == 1)
                    {
                        return employeeModel1;
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
            finally
            {
                this.sqlConnectionVariable.Close();
            }
        }

        /// <summary>
        /// Declaration update employee method 
        /// </summary>
        /// <param name="employeeModel">employee model object passing</param>
        /// <returns>Return boolean value</returns>
        public EmployeeModel UpdateEmployee(REmployeeModel employeeModel)
        {
            try
            {
                if (!EmailChecking(employeeModel.EmailId))
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateEmployeeData", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FirstName", employeeModel.Firstname);
                    sqlCommand.Parameters.AddWithValue("@LastName", employeeModel.Lastname);
                    sqlCommand.Parameters.AddWithValue("@EmailId", employeeModel.EmailId);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.mobileNumber);
                    sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
                    sqlCommand.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    this.sqlConnectionVariable.Open();
                    //var response = sqlCommand.ExecuteNonQuery();
                    int status = 1;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        status = sqlDataReader.GetInt32(0);
                        if (status == 1)
                        {
                            this.sqlConnectionVariable.Close();
                            return GetSpecificEmployeeAllDetailes(employeeModel.EmailId);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                this.sqlConnectionVariable.Close();
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Declaration of employee details method
        /// </summary>
        /// <param name="Id">passing id</param>
        /// <returns>return employee model object</returns>
        public EmployeeModel GetSpecificEmployeeDetails(int Id)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();

                SqlCommand sqlCommand = new SqlCommand("spgetAllEmployee", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    //EmployeeModel employeeModel = new EmployeeModel();
                    if (Convert.ToInt32(sqlDataReader["PresentState"]) == 1)
                    {
                        employeeModel.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                        employeeModel.Firstname = sqlDataReader["FirstName"].ToString();
                        employeeModel.Lastname = sqlDataReader["LastName"].ToString();
                        employeeModel.EmailId = sqlDataReader["EmailId"].ToString();
                        employeeModel.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                        employeeModel.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                        employeeModel.Gender = sqlDataReader["Gender"].ToString();
                        employeeModel.DayAndTime = sqlDataReader["ModificationDate"].ToString();
                        //employeeModelsList.Add(employeeModel);
                        break;
                    }
                }
                return employeeModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.sqlConnectionVariable.Close();
            }
        }

        /// <summary>
        /// Define get specific employee all detailes method
        /// </summary>
        /// <param name="EmailId">Passing email id string</param>
        /// <returns>return employee model object</returns>
        public EmployeeModel GetSpecificEmployeeAllDetailes(string EmailId)
        {
            try
            {
                EmployeeModel employee = new EmployeeModel();

                SqlCommand sqlCommand = new SqlCommand("spgetAllEmployee", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {

                    employee.EmailId = sqlDataReader["EmailId"].ToString();
                    if ( EmailId == employee.EmailId)
                    {
                        employee.Firstname = sqlDataReader["FirstName"].ToString();
                        employee.Lastname = sqlDataReader["LastName"].ToString();
                        employee.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                        employee.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                        employee.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                        employee.Gender = sqlDataReader["Gender"].ToString();
                        employee.DayAndTime = sqlDataReader["ModificationDate"].ToString();
                        break;
                    }
                }

                return employee;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.sqlConnectionVariable.Close();
            }
        }

        /// <summary>
        /// Declare email checking method
        /// </summary>
        /// <param name="emailId">Passing email id string</param>
        /// <returns>return boolean value</returns>
        public bool EmailChecking(string emailId)
        {
            //string EmailId;
            SqlCommand sqlCommand = new SqlCommand("spcheckemailId", this.sqlConnectionVariable);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@EmailId", emailId);
            sqlCommand.Parameters.AddWithValue("@Flag", 1);
            this.sqlConnectionVariable.Open();
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            int status = 1;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                status = sqlDataReader.GetInt32(0);
                this.sqlConnectionVariable.Close();
                if (status == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //this.sqlConnectionVariable.Close();
            return true;
        }
    }
}
