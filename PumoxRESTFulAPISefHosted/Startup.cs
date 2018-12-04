using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(RESTFulAPIConsole.Startup))]

namespace RESTFulAPIConsole
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API 
            // Route: /localhost:8080/{controller}/{action}" instead of "api/{controller}/{id}"
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }
}
