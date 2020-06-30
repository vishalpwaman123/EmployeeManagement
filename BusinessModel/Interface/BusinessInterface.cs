
namespace BusinessModel.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommonModel.Models;

    
    public interface BusinessInterface
    {
        
        Task<bool> AddEmployeeData(EmployeeModel employee);

        IList<EmployeeModel> GetAllEmployee();

        Task<bool> DeleteEmployee(EmployeeModel employee);

        Task<bool> UpdateEmployee(EmployeeModel employee);

        //Task<bool> SearchEmployee(EmployeeModel employeeModel);

        IList<EmployeeModel> SearchOneEmployee(EmployeeModel employeeModel);
    }
}
