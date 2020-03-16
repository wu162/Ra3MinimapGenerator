using System;
using System.Net;

namespace MinimapGen.MapGenerator
{
    public class WebClientEx : WebClient
    {
 
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = 5000;
            return request;
        }
    }
}