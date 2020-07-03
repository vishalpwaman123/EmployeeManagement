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

    /// <summary>
    /// Define class
    /// </summary>
    public class EmployeesRepository : RepositoryInterface
    {
    
        /// <summary>
        /// Define connection variable
        /// </summary>
        private static readonly string ConnectionVariable = "Server=DESKTOP-OF8D1IH;Database=EmployeeDatabase;Trusted_Connection=true;MultipleActiveResultSets=True";
        
        /// <summary>
        /// Define sql connection variable
        /// </summary>
        SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionVariable);
        
        /// <summary>
        /// declaration of add employee method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return boolean value</returns>
        public async Task<int> AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                if (GmailChecking(employeeModel.EmailId))
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddEmployeeData", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Firstname", employeeModel.Fname);
                    sqlCommand.Parameters.AddWithValue("@Lastname", employeeModel.Lname);
                    sqlCommand.Parameters.AddWithValue("@EmailID", employeeModel.EmailId);
                    sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.mobileNumber);
                    sqlCommand.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    sqlCommand.Parameters.AddWithValue("@DayAndTime", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    sqlCommand.Parameters.AddWithValue("@Updation", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    this.sqlConnectionVariable.Open();
                    int response = await sqlCommand.ExecuteNonQueryAsync();
                    if (response ==  -1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }else
                {
                    return 2;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
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
                    employeeModel.Fname = sqlDataReader["FirstName"].ToString();
                    employeeModel.Lname = sqlDataReader["LastName"].ToString();
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
        public async Task<bool> DeleteEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spDeleteEmployeeData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", employeeModel.EmpId);
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
                sqlCommand.Parameters.AddWithValue("@FirstName", employeeModel.Fname);
                sqlCommand.Parameters.AddWithValue("@LastName", employeeModel.Lname);
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
        /// Declaration of search one employee method
        /// </summary>
        /// <param name="employeeModel">employee model object</param>
        /// <returns>return list</returns>
        public IList<EmployeeModel> SearchOneEmployee(EmployeeModel employeeModel)
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
                    EmployeeModel employeeModel1 = new EmployeeModel();
                    employeeModel1.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                    if (employeeModel.EmpId == employeeModel1.EmpId)
                    {
                        employeeModel1.Fname = sqlDataReader["FirstName"].ToString();
                        employeeModel1.Lname = sqlDataReader["LastName"].ToString();
                        employeeModel1.EmailId = sqlDataReader["EmailId"].ToString();
                        employeeModel1.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                        employeeModel1.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                        employeeModel.Gender = sqlDataReader["Gender"].ToString();
                        employeeModelsList.Add(employeeModel1);
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
                        employee.Fname = sqlDataReader["FirstName"].ToString();
                        employee.Lname = sqlDataReader["LastName"].ToString();
                        employee.EmailId = sqlDataReader["EmailId"].ToString();
                        employee.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
                        employee.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
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

        public bool GmailChecking(string gmailId)
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
