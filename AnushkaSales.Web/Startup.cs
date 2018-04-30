using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnushkaSales.Web.Startup))]
namespace AnushkaSales.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
