using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MNC_RememberWords.Startup))]
namespace MNC_RememberWords
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
