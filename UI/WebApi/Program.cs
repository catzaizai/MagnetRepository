using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = System.Configuration.ConfigurationManager.AppSettings["ServerIp"];
            var port = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];
            using (WebApp.Start<Startup>(
            new StartOptions(url: "http://" + ip + ":" + port) /*{ ServerFactory = "Inf.WebApiServers" }*/))
            {
                Console.WriteLine("Started, Press any key to stop.");
                Console.ReadLine();
                Console.WriteLine("Stopped");
            }
        }
    }
}
