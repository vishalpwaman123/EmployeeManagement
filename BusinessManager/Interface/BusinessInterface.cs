//-------------------------------------------------------------------------
// <copyright file="BusinessInterface.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace BusinessModel.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommonModel.Models;

    /// <summary>
    /// Define Class
    /// </summary>
    public interface BusinessInterface
    {
    
        /// <summary>
        /// Define add employee data methode
        /// </summary>
        /// <param name="employee">passing parameter object of employee model</param>
        /// <returns>return in boolean value</returns>
        Task<int> AddEmployeeData(EmployeeModel employee);

        /// <summary>
        /// Define fet all employee method
        /// </summary>
        /// <returns>return list</returns>
        IList<EmployeeModel> GetAllEmployee();

        /// <summary>
        /// Define delete employee method
        /// </summary>
        /// <param name="employee">passing employee model object</param>
        /// <returns></returns>
        Task<int> DeleteEmployee(EmployeeModel employee);

        /// <summary>
        /// Define Update employee method
        /// </summary>
        /// <param name="employee">passing employee model object</param>
        /// <returns>return list</returns>
        Task<bool> UpdateEmployee(EmployeeModel employee);

        /// <summary>
        /// Define employee detail method
        /// </summary>
        /// <param name="Id">passing id</param>
        /// <returns>return employee model object</returns>
        EmployeeModel GetSpecificEmployeeDetails(int Id);

        /// <summary>
        /// Define search one employee method 
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return list</returns>
        IList<EmployeeModel> SearchOneEmployee(EmployeeModel employeeModel);
    }
}
