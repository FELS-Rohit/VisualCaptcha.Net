using Microsoft.Owin;
using Owin;
using VisualCaptchaNet.Mvc5;

[assembly: OwinStartup(typeof(Startup))]
namespace VisualCaptchaNet.Mvc5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
