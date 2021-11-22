using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ecart.Startup))]
namespace Ecart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
