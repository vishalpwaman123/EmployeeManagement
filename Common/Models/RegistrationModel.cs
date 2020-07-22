//-------------------------------------------------------------------------
// <copyright file="RegistrationModel.cs" company="BridgeLab">
//      Copyright (c) Company. All rights reserved.
// </copyright>
// <author>Vishal Waman</author>
//-------------------------------------------------------------------------

namespace CommonModel.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define class
    /// </summary>
    public class RegistrationModel
    {
        /// <summary>
        /// define employee id variable
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// define first name variable
        /// </summary> 
        public string Firstname { get; set; }

        /// <summary>
        /// define last name variable
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// define employee id variable
        /// </summary>
        [RegularExpression("^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$", ErrorMessage = "EmailId is not valid")] 
        public string EmailId { get; set; }

        /// <summary>
        /// define user password variable
        /// </summary>
        //[Required(ErrorMessage = "Required")]
        //[StringLength(maximumLength: 4, MinimumLength = 10,
        //ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string UserPassword { get; set; }

        /// <summary>
        /// define mobile number variable
        /// </summary>
        [RegularExpression("([1-9]{1}[0-9]{9})$", ErrorMessage = "Phone number is not valid")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// define current address variable
        /// </summary>
        public string CurrentAddress { get; set; }

        /// <summary>
        /// define gender variable
        /// </summary>
        [StringLength(maximumLength: 6, MinimumLength = 4,
        ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Gender { get; set; }

        public string DayAndTime { get; set; }
    }
}
