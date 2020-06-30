
namespace RepositoryModel.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using CommonModel.Models;
    using RepositoryModel.Interface;

    
    public class EmployeesRepository : RepositoryInterface
    {
    
        private static readonly string ConnectionVariable = "Server=DESKTOP-OF8D1IH;Database=EmployeeDatabase;Trusted_Connection=true;MultipleActiveResultSets=True";
        SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionVariable);
        public async Task<bool> AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spAddEmployeeData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Firstname", employeeModel.Fname);
                sqlCommand.Parameters.AddWithValue("@Lastname", employeeModel.Lname);
                sqlCommand.Parameters.AddWithValue("@EmailID", employeeModel.EmailId);
                sqlCommand.Parameters.AddWithValue("@UserPassword", employeeModel.UserPassword);
                sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.mobileNumber);
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
                    employeeModel.UserPassword = sqlDataReader["UserPassword"].ToString();
                    employeeModel.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                    employeeModel.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
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


        public async Task<bool> DeleteEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spDeleteEmployeeData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", employeeModel.EmpId);
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
            finally
            {
                this.sqlConnectionVariable.Close();
            }
        }


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
                sqlCommand.Parameters.AddWithValue("@UserPassword", employeeModel.UserPassword);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.mobileNumber);
                sqlCommand.Parameters.AddWithValue("@CurrentAddress", employeeModel.CurrentAddress);
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

        /*public async Task<bool> SearchEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spSearchEmployeeData", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", employeeModel.EmpId);
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
            finally
            {
                this.sqlConnectionVariable.Close();
            }
        }*/

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
                        employeeModel1.UserPassword = sqlDataReader["UserPassword"].ToString();
                        employeeModel1.mobileNumber = Convert.ToInt64(sqlDataReader["MobileNumber"]);
                        employeeModel1.CurrentAddress = sqlDataReader["CurrentAddress"].ToString();
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
    }
}
