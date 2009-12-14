using System;
using System.Diagnostics;
using System.Net;
using NUnit.Framework;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class WebClientFixture
    {
        [Test]
        public void WebClientTest()
        {
            var client = new WebClient();
            //client.Headers.Add();
            client.Headers.Add(HttpRequestHeader.Accept, "application/json");
            string s = client.DownloadString(new Uri(@"http://localhost:2691/ProductCatalog.svc/ProductDetailViewModels/?$top=10"));
            
            Debug.WriteLine(s);
        }
    }
}