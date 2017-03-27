using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BBD.Web.Startup))]
namespace BBD.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
