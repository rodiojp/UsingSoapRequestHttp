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
    public class ContinentInfoService
    {
        private readonly XmlDocument xmlResult;

        public ContinentInfoService(XmlDocument xmlResult)
        {
            this.xmlResult = xmlResult ?? throw new ArgumentNullException(nameof(xmlResult));
        }
        public List<string> GetContinets()
        {
            XmlNodeList elemList = xmlResult.GetElementsByTagName("m:sName");
            //The same result:
            //List<string> list = elemList.Cast<XmlNode>().Select(x => x.InnerText).ToList();
            List<string> list = (from XmlNode item in elemList
                                 select item.InnerText).ToList();
            return list;
        }
    }
}
