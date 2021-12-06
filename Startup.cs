using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Availity.Startup))]
namespace Availity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
