using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Easycase.Web.Startup))]
namespace Easycase.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
