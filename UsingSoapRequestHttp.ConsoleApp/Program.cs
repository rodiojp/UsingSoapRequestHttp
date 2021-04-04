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
    class Program
    {
        private const string requestUriString = @"http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso";
        private const string soapRequestBody11String = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                                            <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                                                <soap:Body>
                                                                <ListOfContinentsByName xmlns=""http://www.oorsprong.org/websamples.countryinfo"">
                                                                </ListOfContinentsByName>
                                                                </soap:Body>
                                                            </soap:Envelope>";
        private const string soapRequestBody12String = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                                            <soap12:Envelope xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                                                              <soap12:Body>
                                                                <ListOfContinentsByName xmlns=""http://www.oorsprong.org/websamples.countryinfo"">
                                                                </ListOfContinentsByName>
                                                              </soap12:Body>
                                                            </soap12:Envelope>";
        static void Main(string[] args)
        {
            HttpSOAPWebRequest request = new HttpSOAPWebRequest((HttpWebRequest)WebRequest.Create(requestUriString));
            //SOAP body request
            XmlDocument soapRequestBody = new XmlDocument();
            //soapRequestBody.LoadXml(soapRequestBody11String);
            soapRequestBody.LoadXml(soapRequestBody12String);
            //Getting Stream Response from request
            //Stream requestStream = request.GetSOAP11ResponseStream(soapRequestBody);
            Stream requestStream = request.GetSOAP12ResponseStream(soapRequestBody);
            string strResult = null;
            using (StreamReader reader = new StreamReader(requestStream))
            {
                //reading stream
                strResult = reader.ReadToEnd();
            }
            //writing stream result on XML
            XmlDocument xmlResult = new XmlDocument();
            xmlResult.LoadXml(strResult);
            Console.WriteLine("List of continents:");
            ContinentInfoService service = new ContinentInfoService(xmlResult);
            IList<string> list = service.GetContinets();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
