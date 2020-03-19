using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_sem_3.App_Start.Startup))]
namespace Project_sem_3.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
