using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QnAFitProject.Startup))]
namespace QnAFitProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
