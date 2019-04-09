using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Xamine.Startup))]
namespace Xamine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
