using System.Text.RegularExpressions;
using System.Web;

namespace Inf.WebApiServers
{
    public class RequestAnalysis
    {
        public string RequestType { get; }

        public string RequestMethod { get; }

        public string RequestParam { get; set; }

        public RequestAnalysis(string requestStr)
        {
            
            var rx = new Regex(@"GET\s+/.*?\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match match = rx.Match(requestStr);
            var str = match.Value.Trim();
            str = HttpUtility.UrlDecode(str) ?? "";
            var strList = str.Split(' ');
            if (strList.Length <= 1) return;
            RequestType = strList[0];
            RequestMethod = strList[1];
            var methodSplit = RequestMethod.Split('/');
            RequestParam = methodSplit[methodSplit.Length - 1];
        }
    }
}
