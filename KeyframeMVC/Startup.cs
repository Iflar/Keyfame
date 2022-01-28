using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KeyframeMVC.Startup))]
namespace KeyframeMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
