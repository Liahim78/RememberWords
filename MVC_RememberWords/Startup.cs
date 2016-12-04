using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_RememberWords.Startup))]
namespace MVC_RememberWords
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
