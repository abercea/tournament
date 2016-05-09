using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportLife.Startup))]
namespace SportLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
