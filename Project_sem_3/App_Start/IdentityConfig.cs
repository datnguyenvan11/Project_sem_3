using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Project_sem_3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Project_sem_3.App_Start
{
    public class IdentityConfig
    {

    }
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("tatthanh0304@gmail.com", "hsvpqrpifdhcosoc");
            smtpServer.Port = 587; // Gmail works on this port
            smtpServer.EnableSsl = true;

            mail.From = new MailAddress("tatthanh0304@gmail.com");
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            smtpServer.Send(mail);
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class MyUserManager : UserManager<Customer>
    {
        public MyUserManager(IUserStore<Customer> store)
            : base(store)
        {
        }

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            var manager = new MyUserManager(new UserStore<Customer>(context.Get<MyDb>()));
            // Configure validation logic for usernames

            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<Customer>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<Customer>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            // Configure validation logic for password
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here. 
            return manager;
        }
    }


}