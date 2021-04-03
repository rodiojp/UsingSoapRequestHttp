using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSoapRequestHttp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CountryInfoService service = new CountryInfoService();
            Console.WriteLine("List of continents:");
            IList<string> list = service.InvokeService();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
