using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Inf.WebApiServers
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class CustomServer
    {
        private IPAddress _localAddress;

        private IPEndPoint _endPoint;

        private TcpListener _tcpListener;

        public bool Started { get; private set; }

        public CustomServer()
        {
            var ip = System.Configuration.ConfigurationManager.AppSettings["ServerIp"];
            var port = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];
            
            _localAddress = string.IsNullOrEmpty(ip) ? IPAddress.Loopback : IPAddress.Parse(ip).MapToIPv4();
            _endPoint = string.IsNullOrEmpty(port)
                ? new IPEndPoint(_localAddress, 0)
                : new IPEndPoint(_localAddress, Convert.ToInt32(port));
            _tcpListener = new TcpListener(_endPoint);
            Started = false;
        }

        public void Start(AppFunc next, IList<IDictionary<string, object>> addresses)
        {
            _tcpListener.Stop();
            do
            {
                Started = ListenStart();
            } while (Started);
        }

        private bool ListenStart()
        {
            _tcpListener.Start();
            Console.WriteLine("Wait an connect Request...");
            var client = _tcpListener.AcceptTcpClient();
            try
            {
                if (client.Connected)
                {
                    Console.WriteLine("Created connection");
                }
                var netStream = client.GetStream();
                var buffer = new byte[2048];
                int receiveLength = netStream.Read(buffer, 0, 2048);
                var requestString = Encoding.UTF8.GetString(buffer, 0, receiveLength);

                Console.WriteLine(requestString);

                var statusLine = "HTTP/1.1 200 OK\r\n";
                var responseStatusLineBytes = Encoding.UTF8.GetBytes(statusLine);
                string responseBody =
                    "<html><head><title>Default Page</title></head><body><p style='font:bold;font-size:24pt'>Welcome my custom server</p></body></html>";
                string responseHeader =
                    $"Content-Type: text/html; charset=UTF-8\r\nContent-Length:{responseBody.Length}\r\n";
                var responseHeaderBytes = Encoding.UTF8.GetBytes(responseHeader);
                var responseBodyBytes = Encoding.UTF8.GetBytes(responseBody);

                netStream.Write(responseStatusLineBytes, 0, responseStatusLineBytes.Length);
                netStream.Write(responseHeaderBytes, 0, responseHeaderBytes.Length);
                netStream.Write(new byte[] { 13, 10 }, 0, 2);
                netStream.Write(responseBodyBytes, 0, responseBodyBytes.Length);
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}

