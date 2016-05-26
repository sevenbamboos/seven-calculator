using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculatorWeb2.Startup))]
namespace CalculatorWeb2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
