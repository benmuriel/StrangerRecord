using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StrangerRecord.Startup))]
namespace StrangerRecord
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
