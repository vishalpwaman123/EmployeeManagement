//-------------------------------------------------------------------------
// <copyright file="EmployeesRepository.cs" company="BridgeLab">
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
    public class EmployeesRepository : RepositoryInterface
    {

        private SqlConnection sqlConnectionVariable;

        public EmployeesRepository()
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
        /// Define connection variable
        /// </summary>
        //private static readonly string ConnectionVariable = "Server=DESKTOP-OF8D1IH;Database=EmployeeDatabase;Trusted_Connection=true;MultipleActiveResultSets=True";

        /// <summary>
        /// Define sql connection variable
        /// </summary>
        //SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionVariable);

        /// <summary>
        /// declaration of add employee method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return boolean value</returns>
        public EmployeeModel AddEmployee(EmployeeModel employeeModel)
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
                    sqlCommand.Parameters.AddWithValue("@DayAndTime", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    sqlCommand.Parameters.AddWithValue("@Updation", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    this.sqlConnectionVariable.Open();
                    int response =  sqlCommand.ExecuteNonQuery();
                    this.sqlConnectionVariable.Close();
                    
                    if (response ==  -1)
                    {
                        return GetSpecificEmployeeAllDetailes(employeeModel.EmailId);
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
            }finally
            {
                this.sqlConnectionVariable.Close();
            }
       
        }



        /// <summary>
        /// Declaration of get all employee method
        /// </summary>
        /// <returns>return list</returns>
        public IList<EmployeeModel> GetAllEmployee()
        {
            try
            {
                IList<EmployeeModel> employeeModelsList = new List<EmployeeModel>();
                SqlCommand sqlCommand = new SqlCommand("spgetAllEmployee", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnectionVariable.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    EmployeeModel employeeModel = new EmployeeModel();
                    employeeModel.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                    employeeModel.Firstname = sqlDataReader["FirstName"].ToString();
                    employeeModel.Lastname = sqlDataReader["LastName"].ToString();
                    employeeModel.EmailId = sqlDataReader["EmailId"].ToString();
                    employeeModel.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                    employeeModel.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                    employeeModel.Gender = sqlDataReader["Gender"].ToString();
                    employeeModelsList.Add(employeeModel);
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
                EmployeeModel employeeModel1 = new EmployeeModel();
                employeeModel1 = GetSpecificEmployeeDetails(EmpId);
                SqlCommand sqlCommand = new SqlCommand("spDeleteEmployeeData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", EmpId);
                this.sqlConnectionVariable.Open();
                var response = sqlCommand.ExecuteNonQuery();
               
                if (response == -1)
                {
                    return employeeModel1;
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
        public async Task<bool> UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spUpdateEmployeeData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", employeeModel.EmpId);
                sqlCommand.Parameters.AddWithValue("@FirstName", employeeModel.Firstname);
                sqlCommand.Parameters.AddWithValue("@LastName", employeeModel.Lastname);
                sqlCommand.Parameters.AddWithValue("@EmailId", employeeModel.EmailId);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.mobileNumber);
                sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
                sqlCommand.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                sqlCommand.Parameters.AddWithValue("@Updation", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                this.sqlConnectionVariable.Open();
                var response = await sqlCommand.ExecuteNonQueryAsync();
                if (response == -1)
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
        /// Declaration of employee details method
        /// </summary>
        /// <param name="Id">passing id</param>
        /// <returns>return employee model object</returns>
        public EmployeeModel GetSpecificEmployeeDetails(int Id)
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

                    employee.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                    if (Id == employee.EmpId)
                    {
                        employee.Firstname = sqlDataReader["FirstName"].ToString();
                        employee.Lastname = sqlDataReader["LastName"].ToString();
                        employee.EmailId = sqlDataReader["EmailId"].ToString();
                        employee.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                        employee.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                        employee.Gender = sqlDataReader["Gender"].ToString();
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

        public bool EmailChecking(string gmailId)
        {
            string EmailId;
            SqlCommand sqlCommand = new SqlCommand("spcheckemailId", this.sqlConnectionVariable);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlConnectionVariable.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                EmailId = sqlDataReader["EmailId"].ToString(); 
                if(EmailId == gmailId)
                {
                    return false;
                }
            }
            this.sqlConnectionVariable.Close();
            return true;
        }
    }
}
