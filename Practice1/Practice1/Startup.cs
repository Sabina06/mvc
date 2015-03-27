using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Practice1.Startup))]
namespace Practice1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
