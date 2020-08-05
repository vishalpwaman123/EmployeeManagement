
namespace CommonModel.RequestModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// Define reset password model class
    /// </summary>
    public class ResetPasswordModel
    {

        /// <summary>
        /// define user password variable
        /// </summary>
        //[RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=]).{6,32}$", ErrorMessage = "New Password Length should be between 6 to 15")]
        [Required]
        public string NewPassword { get; set; }

        /// <summary>
        /// define user password variable
        /// </summary>
        //[RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=]).{6,32}$", ErrorMessage = "Confirm Password Length should be between 6 to 15")]
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
