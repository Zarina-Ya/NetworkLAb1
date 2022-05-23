using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace NetworkLAb1
{
    public class System
    {
        Random rand = new Random(90231231);
        int r = 3;

        List<int> gx = new List<int> { 1, 0, 1, 1 };
        float esp = 0.01f;
        int N;
        

        public System()
       {
            N = (int)(9 / (4 * Math.Pow(esp, 2)));
            Console.WriteLine(N);
            //N = ((int)Math.Round(9 / (4 * Math.Pow(esp, 2))));
        }

        private void Shift(List<int> message, int add)
        {

            for (int i = 0; i < add; i++)
            {
                message.Add(0);
            }

        }
        private List<int> CreateCodeWord(List<int> message)
        {

            List<int> newMes = new List<int>();

            for (int i = 0; i < message.Count; i++)
            {
                newMes.Add(message[i]);
            }

            Shift(newMes, r);

            
            var remains = Polynomial.Division(newMes, gx);
            //Console.Write($"C(X):");
            //PrintPoly(remains);
            //Console.Write($"M(X):");
            //PrintPoly(message);

            var ax = Polynomial.PluseOrXOR(newMes, remains,true);
            //Console.Write($"a(X):");
            //PrintPoly(ax);

            return ax;
            //Console.ReadLine();
        }


        private bool CalculateSyndrome(List<int> bx )
        {
            var remains = Polynomial.Division(bx, gx);
            //Console.WriteLine("remains: ");
            //PrintPoly(remains);
            return IsNull(remains);
        }

        private bool IsNull(List<int> tmp)
        {
            foreach(var x in tmp)
            {
                if(x != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckEIsNull(List<int> e)
        {
            return IsNull(e);
        }

        private bool CheckDecodeError(List<int> bx, List<int> e)
        {
            //Console.WriteLine("bx:");
            //PrintPoly(bx);
            //Console.WriteLine(CalculateSyndrome(bx));
            //Console.WriteLine("ex");
            //PrintPoly(e); //Какая-то ошибка
            //Console.WriteLine(CheckEIsNull(e));
            return (CalculateSyndrome(bx) == true && CheckEIsNull(e) == false);
            //return (CalculateSyndrome(bx) != CheckEIsNull(e));
        }


        private float FindProbDecodError(List<int> message, float prob)
        {
            var ax = CreateCodeWord(message);
            var l = ax.Count;
            int error = 0;
            int NotError = 0;
            int errorOther = 0;
            for (int i = 0; i < N; i++)
            {
                var e = GeneratingRandomMessage(l, prob);
                var bx = Polynomial.PluseOrXOR(ax, e, true);
                if (CheckDecodeError(bx, e))
                {
                    //Console.WriteLine("+");
                    error++;
                } else
                {
                    if (IsNull(e))
                        NotError++;
                    else errorOther++;
                }

            }
            Console.WriteLine($"Error: {error}");
            Console.WriteLine($"NotError: {NotError}");
            Console.WriteLine($"errorOther: {errorOther}");
            //var er = (float)(error / N);

            //float ero = (float)Math.Round(er, 5);
            return (float)((float)error/N);
            //return ero;
        }


        public void GGGGGGGG(List<int> message)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"mes:");
                PrintPoly(message);
                var ax = CreateCodeWord(message);
                Console.WriteLine($"ax: ");
                PrintPoly(ax);

                var e = GeneratingRandomMessage(ax.Count, 0.5f);
                Console.WriteLine($"e: ");
                PrintPoly(e);
                var bx = Polynomial.PluseOrXOR(ax, e, true);
                Console.WriteLine($"bx: ");
                PrintPoly(bx);

                if (CheckDecodeError(bx, e))
                {
                    Console.WriteLine("+");

                }
                Console.WriteLine("\n\n");
            }
        }


        public void EncodingDecoding(int minLenght, int maxLenght)
        {
            List<float> probabOfErrors = new List<float>();
            List<float> probs = new List<float>() { 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
            //List<float> probs = new List<float>() {  0.2f, 0.5f};
            var min = minLenght;
            var max = maxLenght;
            for(int i = min; i <= max; i++)
            {
                Console.WriteLine($"i = {i}");
                foreach(var j in probs)
                {
                    var message = GeneratingRandomMessage(i, 0.5f);
                   // PrintPoly(message);
                    probabOfErrors.Add(FindProbDecodError(message, j));
                }
                AddFile(probs, probabOfErrors, i);
                probabOfErrors.Clear();
            }
        }
        private List<int> GeneratingRandomMessage(int lenght, float prob)
        {
            List<int> message = new List<int>();
            for (int i = 0; i < lenght; i++)
            {
                if (rand.NextDouble() < prob)
                {
                    message.Add(1);
                }
                else message.Add(0);
            }
            return message;
        }

        private void PrintPoly(List<int> poly)
        {
            foreach (int i in poly)
            {
                Console.Write($" {i}");

            }
            Console.WriteLine();
        }



        private void AddFile(List<float> probs, List<float> probabOfErrors, int l)
        {
            string path =" C:\\Users\\zarin\\source\\repos\\NetworkLAb1\\NetworkLAb1\\" + $"{l}" + ".txt";
            //FileStream fs = File.Create(path);
            //fs.Close();
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < probabOfErrors.Count; i++)
            {
                sw.WriteLine(probs[i] + " " + probabOfErrors[i]);
            }

            sw.Close();
        }
    }
}
