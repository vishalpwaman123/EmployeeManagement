
namespace CommonModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// Define register user model class
    /// </summary>
    public class RUserModel
    {
        
        /// <summary>
        /// define first name variable
        /// </summary> 
        [Required]
        [RegularExpression("([a-zA-Z]{3,})$", ErrorMessage = "First Name is not valid")]
        public string Firstname { get; set; }

        /// <summary>
        /// define last name variable
        /// </summary>
        [Required]
        [RegularExpression("([a-zA-Z]{3,})$", ErrorMessage = "Last Name is not valid")]
        public string Lastname { get; set; }

        /// <summary>
        /// define employee id variable
        /// </summary>
        [Required]
        [RegularExpression("^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$", ErrorMessage = "EmailId is not valid")]
        public string EmailId { get; set; }

        /// <summary>
        /// define user password variable
        /// </summary>
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&])[A-Za-z0-9@$!%*?&]{8,10}$", ErrorMessage = "Password Length should be between 6 to 15")]
        [Required]
        public string UserPassword { get; set; }

        /// <summary>
        /// define mobile number variable
        /// </summary>
        [Required]
        [RegularExpression("([1-9]{1}[0-9]{9})$", ErrorMessage = "Phone number is not valid")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// define current address variable
        /// </summary>
        [Required]
        [RegularExpression("([a-zA-Z]{3,})$", ErrorMessage = "Current Address is not valid")]
        public string CurrentAddress { get; set; }

        /// <summary>
        /// define gender variable
        /// </summary>
        [Required]
        [RegularExpression("^(?:m|M|male|Male|f|F|female|Female)$", ErrorMessage = "Not valid Gender eg : Male Or Female")]
        public string Gender { get; set; }

    }
}
