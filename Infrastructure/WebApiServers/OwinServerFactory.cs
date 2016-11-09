using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inf.WebApiServers
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public static class OwinServerFactory
    {
        public static void Initialize(IDictionary<string, object> properties)
        {
            properties[typeof(CustomServer).FullName] = new CustomServer();
        }

        public static CustomServer Create(AppFunc app, IDictionary<string, object> properties)
        {
            object obj;
            CustomServer server = null;
            if (properties.TryGetValue(typeof (CustomServer).FullName, out obj))
            {
                server = obj as CustomServer;
            }
            server = server ?? new CustomServer();

            IList<IDictionary<string, object>> addresses = null;
            if (properties.TryGetValue("host.Addresses", out obj))
            {
                addresses = obj as IList<IDictionary<string, object>>;
            }
            server.Start(app, addresses);
            return server;
        }
    }
}
