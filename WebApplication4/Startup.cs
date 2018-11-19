using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication4.Startup))]
namespace WebApplication4
{
    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
