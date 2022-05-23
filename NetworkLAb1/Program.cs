using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetworkLAb1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> message = new List<int>() { 1, 0, 1, 1 };
            
            NetworkLAb1.System system = new NetworkLAb1.System();
            //system.Shift(gx);
            //system.GGGGGGGG(message);
            system.EncodingDecoding(2, 5);

            Console.ReadKey();
        }
    }
}
