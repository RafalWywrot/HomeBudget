using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeBudget.WebApplication.Startup))]
namespace HomeBudget.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
