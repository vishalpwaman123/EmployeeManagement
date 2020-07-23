//-------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace CommonModel.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define class
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// define employee id variable
        /// </summary>
        [Required]
        public int EmpId { get; set; }

        /// <summary>
        /// define first name variable
        /// </summary>
        /// 

        [StringLength(maximumLength: 20, MinimumLength = 3,
         ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Firstname { get; set; }

        /// <summary>
        /// define last name variable
        /// </summary>
        [StringLength(maximumLength: 20, MinimumLength = 3,
         ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Lastname { get; set; }

        /// <summary>
        /// define email id variable
        /// </summary>
        [Required]
        [RegularExpression("^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$", ErrorMessage = "EmailId is not valid")]
        public string EmailId { get; set; }

        /// <summary>
        /// define mobile number variable
        /// </summary>
        [RegularExpression("([1-9]{1}[0-9]{9})$", ErrorMessage = "Phone number is not valid")]
        [Required]
        public Int64 mobileNumber { get; set; }

        /// <summary>
        /// define current address variable
        /// </summary>
        [Required]
        [RegularExpression("([a-zA-Z]{2,})$", ErrorMessage = "City is not valid")]
        public string CurrentAddress { get; set; }

        /// <summary>
        /// define gender variable
        /// </summary>
        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(?:m|M|male|Male|f|F|female|Female)$", ErrorMessage = "Not valid Gender eg : Male Or Female")]
        public string Gender { get; set; }

        /// <summary>
        /// define date and time variable
        /// </summary>
        public string DayAndTime { get; set; }
    }
}
