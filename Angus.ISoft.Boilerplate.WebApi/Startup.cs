using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Angus.ISoft.Boilerplate.WebApi.Startup))]

namespace Angus.ISoft.Boilerplate.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureOAuth(app);
        }
    }
}
