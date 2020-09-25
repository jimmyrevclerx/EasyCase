using Easycase.Extension;
using Easycase.Model.Email;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    [Authorize]
    [HandleError]
    public class EmailController : BaseController
    {
        // GET: Email
        public ActionResult Inbox()
        {
            return View();
        }
        public ActionResult Compose()
        {
            ViewData["SuccessEmail"] = TempData["SuccessEmail"];
            ViewData["FailedEmail"] = TempData["FailedEmail"];
            BLCompose bLCompose = new BLCompose();
            var userId = User.Identity.GetUserName();
            bLCompose.From = userId;
            return View(bLCompose);
        }

        [HttpPost]
        public ActionResult SendMail(BLCompose bLCompose, List<HttpPostedFileBase> attachments)
        {
            try
            {
                MailMessage mail = new MailMessage();

                foreach (HttpPostedFileBase attachment in attachments)
                {
                    if (attachment != null)
                    {
                        string fileName = Path.GetFileName(attachment.FileName);
                        mail.Attachments.Add(new Attachment(attachment.InputStream, fileName));
                    }
                }
                
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"]);
                mail.From = new MailAddress(bLCompose.From);
                mail.To.Add(bLCompose.To);
                if (!string.IsNullOrEmpty(bLCompose.Cc))
                    mail.CC.Add(bLCompose.Cc);
                mail.Subject = bLCompose.Subject;
                mail.Body = bLCompose.Body;
                SmtpServer.EnableSsl = true;
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Common.Decrypt(ConfigurationManager.AppSettings["Email"]), Common.Decrypt(ConfigurationManager.AppSettings["Password"]));
                SmtpServer.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                SmtpServer.Send(mail);
                TempData["SuccessEmail"] = Messages.MAILSUCCESS;
                return View("Compose");
            }
            catch (Exception ex)
            {
                TempData["FailedEmail"] = ex.Message;
                return View("Compose");
            }
        }
    }
}