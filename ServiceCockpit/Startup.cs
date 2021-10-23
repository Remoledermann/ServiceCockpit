using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceCockpit.Startup))]
namespace ServiceCockpit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }
}
