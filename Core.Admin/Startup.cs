using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Core.Admin.Startup))]
namespace Core.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
