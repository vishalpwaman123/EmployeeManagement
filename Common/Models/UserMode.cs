using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonModel.Models
{
    public class UserMode
    {

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

    }
}
