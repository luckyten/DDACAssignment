using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DDACAssignment.Startup))]
namespace DDACAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
