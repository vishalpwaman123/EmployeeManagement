//-------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="BridgeLab">
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
    public class UserModel
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
        [Required]
        [RegularExpression("^.{3,30}$", ErrorMessage = "Password Length should be between 8 to 15")]
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
        [RegularExpression("^(?:m|M|male|Male|f|F|female|Female)$", ErrorMessage = "Not valid Gender eg : Male Or Female")]
        public string Gender { get; set; }

        public string DayAndTime { get; set; }

        public string Token;
    }
}
