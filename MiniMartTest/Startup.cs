using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniMartTest.Startup))]
namespace MiniMartTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
