using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UsingSoapRequestHttp.ConsoleApp
{
    public class HttpSOAPWebRequest
    {
        private readonly HttpWebRequest request;
        public HttpSOAPWebRequest(HttpWebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //Add the SOAP Action in headers. It determines what action the web service should use 
            request.Headers.Add("SOAPAction", "");
            request.UserAgent = "MyApp";
            //HTTP method
            request.Method = "POST";
            this.request = request;
        }
        private Stream GetResponseStream(XmlDocument soapRequestBody)
        {
            soapRequestBody.Save(request.GetRequestStream());
            return request.GetResponse().GetResponseStream();
        }
        /// <summary>
        /// Making Web Request SOAP 1.1
        /// </summary>
        public Stream GetSOAP11ResponseStream(XmlDocument soapRequestBody)
        {
            if (soapRequestBody is null)
            {
                throw new ArgumentNullException(nameof(soapRequestBody));
            }
            //Making Web Request SOAP 1.1
            request.ContentType = "text/xml; charset=utf-8";
            return GetResponseStream(soapRequestBody);
        }
        /// <summary>
        /// Making Web Request SOAP 1.2
        /// </summary>
        public Stream GetSOAP12ResponseStream(XmlDocument soapRequestBody)
        {
            if (soapRequestBody is null)
            {
                throw new ArgumentNullException(nameof(soapRequestBody));
            }
            request.ContentType = "application/soap+xml; charset=utf-8";
            return GetResponseStream(soapRequestBody);
        }
    }
}
