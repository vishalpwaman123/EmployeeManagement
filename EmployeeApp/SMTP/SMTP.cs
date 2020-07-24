using System;
using System.Net;
using System.Net.Mail;
//using MailKit.Net.Smtp;
using MimeKit;
namespace EmployeeManagement.SMTP
{
    /// <summary>
    /// Class For Sending Email.
    /// </summary>
    public class SMTP
    {
        /// <summary>
        /// Function For Sending Email.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mail"></param>
        /// <param name="data"></param>
        public void SendEmail(string mail, string data)
        {
            /*try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("vishal",mail));
                message.To.Add(new MailboxAddress("Employee Management", "1001thebeast1001@gmail.com"));
                message.Subject = "Registration";
                message.Body = new TextPart("plain")
                {
                    Text = data
                };

                using(var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com",587,false);
                    client.Authenticate("1001thebeast1001@gmail.com", "8806787166");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
                   catch(Exception e)
            {
                throw new Exception(e.Message);
            }*/

            MailMessage message = new MailMessage("1001thebeast1001@gmail.com", mail);
            message.Subject = " Forget Password";
            message.Body = string.Format("Hello : <h1>{0}</h1> is Your Email id <br/> your Token is <h1>{1}</h1>", mail, data);
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential net = new NetworkCredential();
            net.UserName = "1001thebeast1001@gmail.com";
            net.Password = "8806787166";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = net;
            smtp.Port = 587;
            smtp.Send(message);


        }
    }
}
