//-------------------------------------------------------------------------
// <copyright file="IEmployeeRL.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace RepositoryModel.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommonModel.Models;

    /// <summary>
    /// Define interface class
    /// </summary>
    public interface IEmployeeRL
    {
        /// <summary>
        /// Define add employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return boolean value</returns>
        EmployeeModel AddEmployee(REmployeeModel employeeModel);

        /// <summary>
        /// define get all employee method
        /// </summary>
        /// <returns>return list</returns>
        IList<EmployeeModel> GetAllEmployee();

        /// <summary>
        /// Define delete employee method
        /// </summary>
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return boolean value</returns>
        EmployeeModel DeleteEmployee(int EmpId);

        /// <summary>
        /// Define update employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>return boolean value</returns>
        EmployeeModel UpdateEmployee(REmployeeModel employeeModel);

        /// <summary>
        /// Define get employee details method
        /// </summary>
        /// <param name="Id">Passing id value</param>
        /// <returns>return employee model object</returns>
        EmployeeModel GetSpecificEmployeeDetails(int Id);
    }
}
