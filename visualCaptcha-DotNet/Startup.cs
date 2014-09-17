using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(visualCaptcha_DotNet.Startup))]
namespace visualCaptcha_DotNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
