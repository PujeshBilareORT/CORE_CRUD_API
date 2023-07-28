using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using CORE_CRUD_API.Model;
using System.Text;

namespace CORE_CRUD_API.Controllers
{
	[ApiController]
	public class EmailController : ControllerBase
	{
		[HttpPost]
		[Route("api/[controller]/v1/SendEmail")]
        public IActionResult SendEmail(MyUserMasterModel um) 
        {
			string from = "pujesh.k.bilare@gmail.com";
			MailMessage message = new MailMessage(from, um.Email);
			var lnkHref = "<a href='" + Url.Action("UpdatePassword", "ForgotPassword", new { email = um.Email }, "https") + "'>Reset Password</a>";
			message.Subject = "Link to Reset Password:";
			string mailbody = "Attempt For Password Reset..!!   " + lnkHref;
			message.Body = mailbody;
			message.BodyEncoding = Encoding.UTF8;
			message.IsBodyHtml = true;
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			NetworkCredential basicCredential1 = new NetworkCredential("pujesh.k.bilare@gmail.com", "flnvqvmlkleihymo");
			client.EnableSsl = true;
			client.UseDefaultCredentials = false;
			client.Credentials = basicCredential1;
			try
			{
				client.Send(message);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return Ok("Mail Sent");
		}
	}
}
