using System;
using System.Threading.Tasks;
using Inf.SearchTool;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.Run(context =>
            {
                var content = "";
                if (!string.IsNullOrEmpty(context.Request.Path.Value))
                {
                    var keyword = context.Request.Path.Value.Substring(1, context.Request.Path.Value.Length - 1);
                    var result = SearchHelper.Search(keyword);
                    content = JsonConvert.SerializeObject(result);
                }                
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(content);
            });
        }
    }
}
