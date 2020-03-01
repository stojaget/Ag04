using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Feeddit.Startup))]
namespace Feeddit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
