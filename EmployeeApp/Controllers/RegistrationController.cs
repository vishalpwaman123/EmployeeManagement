using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Interface;
using CommonModel.Models;
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
    public class RegistrationController : ControllerBase
    {
        private IConfiguration configuration;
        /// <summary>
        /// define business registration interface object
        /// </summary>
        private readonly BusinessRegistrationInterface employeeBusiness;

        /// <summary>
        /// Declaration of constructor
        /// </summary>
        /// <param name="employeeBusiness"></param>
        public RegistrationController(BusinessRegistrationInterface employeeBusiness , IConfiguration configuration)
        {
            this.employeeBusiness = employeeBusiness;
            this.configuration = configuration;

        }

        /// <summary>
        /// Declaration of add employee method
        /// </summary>
        /// <param name="employeeModel">passing employee model object</param>
        /// <returns>Return action</returns>
        [HttpPost]
        [Route("RegistrationEmployee")]
        public IActionResult AddEmployeeData(RegistrationModel employeeModel)
        {
            try
            {
                var responseMessage =  this.employeeBusiness.AddEmployeeData(employeeModel);
                if (responseMessage != null)
                {
                    bool Success = true;
                    var Message = "RegistrationController SuccessFull";
                    return this.Ok(new { Success, Message, Data = responseMessage });
                }
                else
                {
                    bool Success = false;
                    var Message = "RegistrationController Failed";
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
        /// <param name="employeeModel">Passing employee model object</param>
        /// <returns>return action result</returns>
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> EmployeeLogin(RegistrationModel employeeModel)
        {
            try
            {
                IActionResult response = Unauthorized();
                var responseMessage =  this.employeeBusiness.EmployeeLogin(employeeModel);

                if (responseMessage != null)
                {
                    var tokenString = GenerateJsonWebToken(employeeModel);
                    return Ok(new { token = tokenString });

                    /*bool Success = true;
                    var Message = "User Login SuccessFully";
                    return this.Ok(new { Success, Message, Data = responseMessage });
*/                }
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

        /// <summary>
        /// Function For JsonToken Generation.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GenerateJsonWebToken(RegistrationModel data)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                            configuration["Jwt:Audiance"],
                            null,
                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
