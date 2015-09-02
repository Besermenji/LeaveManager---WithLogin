using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeaveManager___WithLogin.Startup))]
namespace LeaveManager___WithLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
