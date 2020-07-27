using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Interface;
using CommonModel.Models;
using CommonModel.RequestModels;
using EmployeeManagement.SMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SimpleApplication.Controllers
{
    /// <summary>
    /// Define class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration configuration;
        public string EmailId = null ;
        /// <summary>
        /// define business registration interface object
        /// </summary>
        private readonly IUserBL employeeBusiness;

        /// <summary>
        /// Declaration of constructor
        /// </summary>
        /// <param name="employeeBusiness"></param>
        public UserController(IUserBL employeeBusiness , IConfiguration configuration)
        {
            this.employeeBusiness = employeeBusiness;
            this.configuration = configuration;
        }

        Sender senderObject = new Sender();
        SMTP smtpObject = new SMTP();
        /// <summary>
        /// Declaration of add employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>Return action</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult AddUser(RUserModel UserModel)
        {
            try
            {
                var responseMessage =  this.employeeBusiness.AddEmployeeData(UserModel);
                if (responseMessage != null)
                {
                    //Message For MSMQ.
                    /*string message = "  Hello " + Convert.ToString(UserModel.Firstname) + " " + Convert.ToString(UserModel.Lastname) +
                                     "\n Your " + "Registration Succesful" +
                                     "\n Email :" + Convert.ToString(UserModel.EmailId) +
                                     "\n MobileNumber: " + Convert.ToString(UserModel.MobileNumber) +
                                     "\n CurrentAddress:  " + Convert.ToString(UserModel.CurrentAddress) +
                                     "\n Gender : " + Convert.ToString(UserModel.Gender);

                    //Sending Message To MSMQ.
                    senderObject.Send(message);*/

                    bool Success = true;
                    var Message = "UserController SuccessFull";
                    return this.Ok(new { Success, Message, Data = responseMessage });
                }
                else
                {
                    bool Success = false;
                    var Message = "UserController Failed";
                    return this.BadRequest(new { Success, Message , Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
        }

        /// <summary>
        /// Declaration of employee login method
        /// </summary>
        /// <param name="UserModel">Passing employee model object</param>
        /// <returns>return action result</returns>
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult UserLogin(UserMode UserModel)
        {
            try
            {
                //IActionResult response = Unauthorized();
                var responseMessage =  this.employeeBusiness.UserLogin(UserModel);

                if (responseMessage != null)
                {
                    var tokenString = GenerateJsonWebToken(UserModel.EmailId, "user authenticate");
                    responseMessage.Token = tokenString;

                    bool Success = true;
                    var Message = "User Login SuccessFully";
                    return this.Ok(new { Success, Message, data = responseMessage });

                }
                else
                {
                    bool Success = false;
                    var Message = "User Login Failed";
                    return this.BadRequest(new { Success, Message , Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
        }

        [AllowAnonymous]
        [Route("ForgetPassword")]
        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordModel forgetpasswordModel)
        {
            try
            {

                var responseMessage =  this.employeeBusiness.ForgetPassword(forgetpasswordModel);
                if (responseMessage == true)
                {
                    string tokenString = GenerateJsonWebToken(forgetpasswordModel.EmailId, "user authenticate");

                    //Message For MSMQ.
                    string message = "  Token : " + tokenString +
                                     "\n Email :" + Convert.ToString(forgetpasswordModel.EmailId);
                                       
                    //Sending Message To MSMQ.
                    senderObject.Send(message);
                    smtpObject.SendEmail(forgetpasswordModel.EmailId, tokenString);

                    senderObject.clears();
                    string message1 = Convert.ToString(forgetpasswordModel.EmailId);
                    senderObject.Senders(message1);

                    bool Success = true;
                    var Message = "Password Send On User Email Address SuccessFully";
                    return this.Ok(new { Success, Message });

                }
                else
                {
                    bool Success = false;
                    var Message = "Invalid Email Id";
                    return this.BadRequest(new { Success, Message, Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }
            
        }

        [Authorize]
        [Route("ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                EmailId = senderObject.Receivers();
                var responseMessage = this.employeeBusiness.ResetPassword(resetPasswordModel, EmailId);
                if (responseMessage == true)
                {
                    
                    bool Success = true;
                    var Message = "Reset Password SuccessFully";
                    return this.Ok(new { Success, Message });

                }
                else
                {
                    bool Success = false;
                    var Message = "Reset Password UnSuccessFully";
                    return this.BadRequest(new { Success, Message, Data = responseMessage });
                }
            }
            catch (Exception e)
            {
                bool Success = false;
                return this.BadRequest(new { Success, message = e.Message });
            }

        }


        /// <summary>
        /// Function For JsonToken Generation.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GenerateJsonWebToken(string data, string type)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("EmailId", data));
            claims.Add(new Claim("TokenType", type));

            var token = new JwtSecurityToken(
                            claims : claims,
                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
