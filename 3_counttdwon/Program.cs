using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _3_counttdwon
{
    class Program
    {
        static CountdownEvent _countdown = new CountdownEvent(3);

        static void Main()
        {
            new Thread(Metodus).Start("1. szál");
            new Thread(Metodus).Start("2. szál");
            new Thread(Metodus).Start("3. szál");

            _countdown.Wait();
            Console.WriteLine("mindegyik szál befejeződött");
        }

        static void Metodus(object parameter)
        {
            Thread.Sleep(1000);
            Console.WriteLine(parameter);
            _countdown.Signal();
        }
    }
}
