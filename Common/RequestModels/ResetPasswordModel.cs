
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
        [RegularExpression("^.{3,30}$", ErrorMessage = "Password Length should be between 3 to 15")]
        [Required]
        public string NewPassword { get; set; }

        /// <summary>
        /// define user password variable
        /// </summary>
        [RegularExpression("^.{3,30}$", ErrorMessage = "Password Length should be between 3 to 15")]
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
