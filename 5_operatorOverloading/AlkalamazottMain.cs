using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_operatorOverloading
{
    class AlkalamazottMain
    {
        static void Main(string[] args)
        {
            Alkalmazott alk1 = new Alkalmazott("a", 10);
            Alkalmazott alk2 = new Alkalmazott("b", 11);
            Alkalmazott alk3 = alk1 + alk2;
            Console.WriteLine(alk1 != alk2);
            Console.WriteLine(alk3);
            alk3++;
            Console.WriteLine(alk3);
            Console.WriteLine(alk1 < alk2);

            Alkalmazott alkStr = (Alkalmazott)"10";
            Console.WriteLine(alkStr);
            Console.WriteLine((long)alkStr);

            Alkalmazott alk5 = 42L;
            Console.WriteLine(alk5);
        }
    }
}
