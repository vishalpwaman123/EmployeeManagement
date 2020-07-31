

namespace CommonModel.ResponseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class UserResponseModel
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
        //[RegularExpression("^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$", ErrorMessage = "EmailId is not valid")]
        public string EmailId { get; set; }

        /// <summary>
        /// define mobile number variable
        /// </summary>
        //[RegularExpression("([1-9]{1}[0-9]{9})$", ErrorMessage = "Phone number is not valid")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// define current address variable
        /// </summary>
        public string CurrentAddress { get; set; }

        /// <summary>
        /// define gender variable
        /// </summary>
        //[RegularExpression("^(?:m|M|male|Male|f|F|female|Female)$", ErrorMessage = "Not valid Gender eg : Male Or Female")]
        public string Gender { get; set; }

        /// <summary>
        /// Define day and time variable
        /// </summary>
        public string DayAndTime { get; set; }

    }
}
