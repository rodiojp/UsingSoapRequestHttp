using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UsingSoapRequestHttp.ConsoleApp
{
    public class ContinentInfoService1
    {
        //<soap:Envelope xmlns:soap="http://www.w3.org/2003/05/soap-envelope">
        //  <soap:Body>
        //    <m:ListOfContinentsByNameResponse xmlns:m="http://www.oorsprong.org/websamples.countryinfo">
        //      <m:ListOfContinentsByNameResult>
        //        <m:tContinent>
        //          <m:sCode>AF</m:sCode>
        //          <m:sName>Africa</m:sName>
        //        </m:tContinent>
        //        <m:tContinent>
        //          <m:sCode>AN</m:sCode>
        //          <m:sName>Antarctica</m:sName>
        //        </m:tContinent>
        //        <m:tContinent>
        //          <m:sCode>AS</m:sCode>
        //          <m:sName>Asia</m:sName>
        //        </m:tContinent>
        //        <m:tContinent>
        //          <m:sCode>EU</m:sCode>
        //          <m:sName>Europe</m:sName>
        //        </m:tContinent>
        //        <m:tContinent>
        //          <m:sCode>OC</m:sCode>
        //          <m:sName>Ocenania</m:sName>
        //        </m:tContinent>
        //        <m:tContinent>
        //          <m:sCode>AM</m:sCode>
        //          <m:sName>The Americas</m:sName>
        //        </m:tContinent>
        //      </m:ListOfContinentsByNameResult>
        //    </m:ListOfContinentsByNameResponse>
        //  </soap:Body>
        //</soap:Envelope>

        public List<string> InvokeService()
        {
            // from <endpoint address="http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso"
            HttpWebRequest request = CreateSOAPWebRequest(@"http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");
            XmlDocument soapRequestBody = new XmlDocument();
            // SOAP body request
            soapRequestBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                            <soap:Body>
                                            <ListOfContinentsByName xmlns=""http://www.oorsprong.org/websamples.countryinfo"">
                                            </ListOfContinentsByName>
                                            </soap:Body>
                                        </soap:Envelope>"
                                    );
            using (Stream stream = request.GetRequestStream())
            {
                soapRequestBody.Save(stream);
            }

            XDocument xmlResult = null;
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                xmlResult = XDocument.Load(stream);
            }
            var lst = xmlResult.Root.Elements("Body");
            XNamespace soap = "http://www.w3.org/2003/05/soap-envelope";
            XNamespace m = "http://www.oorsprong.org/websamples.countryinfo";
            IEnumerable<string> result =
                from o in xmlResult.Elements(soap + "Envelope").
                         Elements(soap + "Body").
                         Elements(m + "ListOfContinentsByNameResponse").
                         Elements(m + "ListOfContinentsByNameResult").
                         Elements(m + "tContinent")
                select (string)o.Element(m + "sName");
            List<string> list = result.ToList();
            return list;
        }

        private HttpWebRequest CreateSOAPWebRequest(string requestUriString)
        {
            //Making Web Request SOAP 1.1
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
            //Add Headers
            //The SOAP Action determines what action the web service should use 
            //This set it here
            request.Headers.Add("SOAPAction", "");
            request.ContentType = "application/soap+xml; charset=utf-8";
            request.UserAgent ="MyApp";
            //HTTP method
            request.Method = "POST";
            return request;
        }
    }
}
