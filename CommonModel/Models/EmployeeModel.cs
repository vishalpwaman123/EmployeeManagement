
namespace CommonModel.Models
{
    using System;

    public class EmployeeModel
    {
        public int EmpId { get; set; }
   
        public string Fname { get; set; }
        
        public string Lname { get; set; }

        public string EmailId { get; set; }

        public string UserPassword { get; set; }

        public Int64 mobileNumber { get; set; }

        public string CurrentAddress { get; set; }
    }
}
