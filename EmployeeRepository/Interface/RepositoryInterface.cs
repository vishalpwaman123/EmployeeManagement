
namespace RepositoryModel.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommonModel.Models;

    public interface RepositoryInterface
    {
        Task<bool> AddEmployee(EmployeeModel employeeModel);

        IList<EmployeeModel> GetAllEmployee();

        Task<bool> DeleteEmployee(EmployeeModel employeeModel);

        Task<bool> UpdateEmployee(EmployeeModel employeeModel);

        //Task<bool> SearchEmployee(EmployeeModel employeeModel);

        IList<EmployeeModel> SearchOneEmployee(EmployeeModel employeeModel);
    }
}
