using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cho500.Startup))]
namespace cho500
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
