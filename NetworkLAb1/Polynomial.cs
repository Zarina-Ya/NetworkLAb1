using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLAb1
{
    public class Polynomial
    {





        public static List<int> Division(List<int> poly1, List<int> poly2)
        {
            List<int> result = new List<int>();
            //List<int> p1 = new List<int>();
            //List<int> p2 = new List<int>();
            foreach (int i in poly1)
            {
                result.Add(i);
            }

            //     while True:
            //    j = deg(dividend)
            //    if j<deg(divider):
            //        break
            //    t = deg(dividend) - deg(divider)
            //    if t != 0:
            //        quotient = plus(quotient, mul([1], t))
            //        tmp = mul(divider, t)
            //    else:
            //        quotient = plus(quotient, [1])
            //        tmp = divider
            //    dividend = xor(dividend, tmp)
            //return dividend

            List<int> remains = new List<int>();
            while (true)
            {
                //if (TakeADegree(poly1) < TakeADegree(poly2)) break ;
                //var degreeDifference = TakeADegree(poly1) - TakeADegree(poly2);
                //remains = Shift(poly2, degreeDifference);
                //poly1 = PluseOrXOR(poly1, remains, false); // f = true Pluse, f = false XOR
                if (TakeADegree(result) < TakeADegree(poly2)) break;
                var degreeDifference = TakeADegree(result) - TakeADegree(poly2);
               
                remains = Shift(poly2, degreeDifference);
                result = PluseOrXOR(result, remains, false); // f = true Pluse, f = false XOR


            }

            return result;
        }

        public static int TakeADegree(List<int> message)
        {
           for (int i = 0; i < message.Count; i++){
                if(message[i] == 1)
                    return message.Count - 1 - i;
           }
            return 0;
        }

        public static List<int> Shift(List<int> message, int add)
        {
            List<int> result = new List<int>();
            foreach (int i in message)
            {
                result.Add(i);
            }
            for (int i = 0; i < add; i++)
            {
                result.Add(0);
            }
            return result;
        }

        public static List<int> PluseOrXOR(List<int> poly1, List<int> poly2, bool f) // f = true Pluse, f = false XOR
        {
            List<int> result = new List<int>();

            List<int> p1 = new List<int>();
            List<int> p2 = new List<int>();


            foreach(int i in poly1)
            {
                p1.Add(i);
            }


            foreach(int i in poly2)
            {
                p2.Add(i);
            }

            if (p1.Count > p2.Count)
            {
                while(p1.Count > p2.Count)
                {
                    p2.Insert(0, 0); 
                }
            }
            else if(p1.Count < p2.Count)
            {
                while (p1.Count < p2.Count)
                {
                    p1.Insert(0, 0);
                }
            }

            if (f) Pluse(result, p2, p1);
            else XOR(result, p2, p1);
            
            return result;
        }


        private static void Pluse(List<int> result, List<int> poly2, List<int> poly1)
        {
            for (int i = poly2.Count - 1; i >= 0; i--)
            {
                result.Insert(0, poly2[i] ^ poly1[i]);
            }

        }
        private static void XOR(List<int> result, List<int> poly2, List<int> poly1)
        {

            for (int i = 0; i < poly1.Count; i++)
            {
                result.Add( poly1[i] ^ poly2[i]);
            }

            if (result.Contains(1))
            {
                while (true)
                {
                    if (result[0] == 1) break;
                    else result.RemoveAt(0);
                }
            }
            else return;

        }

        


    }
}
