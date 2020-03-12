using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Project_sem_3.App_Start;
using Project_sem_3.Models;

[assembly: OwinStartupAttribute(typeof(Project_sem_3.Startup))]
namespace Project_sem_3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(MyDb.Create);
            app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Accounts/Login"),

            });

        }
    }
}
