
namespace BusinessModel.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessModel.Interface;
    using CommonModel.Models;
    using RepositoryModel.Interface;

        public class EmployeeBusiness : BusinessInterface
    {
       
        private readonly RepositoryInterface employeeRepositoryL;

       
        public EmployeeBusiness(RepositoryInterface employeeRepository)
        {
            this.employeeRepositoryL = employeeRepository;
        }

       
        public async Task<bool> AddEmployeeData(EmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
                    var response = await this.employeeRepositoryL.AddEmployee(employeeModel);
                    if (response == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                var response = this.employeeRepositoryL.GetAllEmployee();
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteEmployee(EmployeeModel employee)
        {
            try
            {
                if (employee != null)
                {
                    var response = await this.employeeRepositoryL.DeleteEmployee(employee);
                    if (response == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public async Task<bool> UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                if (employee != null)
                {
                    
                    var response = await this.employeeRepositoryL.UpdateEmployee(employee);
                    
                    if (response == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

       /* public async Task<bool> SearchEmployee(EmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel != null)
                {
                    var response = await this.employeeRepositoryL.SearchEmployee(employeeModel);
                    if (response == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
        }*/

        public IList<EmployeeModel> SearchOneEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var response = this.employeeRepositoryL.SearchOneEmployee(employeeModel);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


    }
}
