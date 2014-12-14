using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wise.MVC.TestApplication.Startup))]
namespace Wise.MVC.TestApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
