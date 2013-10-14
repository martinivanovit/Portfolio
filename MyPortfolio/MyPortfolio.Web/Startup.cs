using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPortfolio.Web.Startup))]
namespace MyPortfolio.Web
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
