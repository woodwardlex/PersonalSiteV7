using PersonalSiteV7.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace PersonalSiteV7.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string body = $"You have recieved an email from {cvm.Name} containing the subject of {cvm.Subject}. Please respond to {cvm.Email} concerning:<br><br> {cvm.Message}";

            MailMessage msg = new MailMessage("admin@lexwoodward.com", "alexis.woodward1717@outlook.com", "Email from lexwoodward.com", body);

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient("mail.lexwoodward.com");
            client.Credentials = new NetworkCredential("admin@lexwoodward.com", "NBHD!2020");

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.SendMailError = $"Sorry, something went wrong! Details: {ex.Message}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }

        public ActionResult Resume()
        {
            return View();
        }
    }
}
