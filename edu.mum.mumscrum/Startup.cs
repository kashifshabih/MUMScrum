using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(edu.mum.mumscrum.Startup))]
namespace edu.mum.mumscrum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
