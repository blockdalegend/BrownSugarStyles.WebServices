using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrownSugarStyles.WebServices.Startup))]
namespace BrownSugarStyles.WebServices
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
