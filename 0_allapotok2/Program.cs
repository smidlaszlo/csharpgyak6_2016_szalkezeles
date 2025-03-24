using System;
using System.Threading;

namespace _0_allapotok
{
    class Program
    {
        private static bool _elsoSzalMegallit = false;
        private static bool _masodikSzalMegallit = false;

        public static void Main(string[] args)
        {
            Thread elsoSzal = new Thread(AbortaloMetodus);
            Thread masodikSzal = new Thread(FelfuggesztoMetodus);

            elsoSzal.Start();
            Thread.Sleep(500);
            Console.WriteLine("Első szál leállítása...(Main)");
            _elsoSzalMegallit = true;
            elsoSzal.Join();
            Console.WriteLine("Első szál befejeződött.");

            masodikSzal.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Második szál felfüggesztése...(Main)");
            _masodikSzalMegallit = true; // Jelzés a szálnak, hogy álljon le, de nem azonnal
            Thread.Sleep(1000);
            _masodikSzalMegallit = false; // Folytatás
            Console.WriteLine("Második szál folytatódik...(Main)");
            masodikSzal.Join();
            Console.WriteLine("Második szál befejeződött.");

            Console.WriteLine("Program vége.");
        }

        static void AbortaloMetodus()
        {
            Console.WriteLine("Első szál elindult.");

            for (int i = 0; i < 10; i++)
            {
                if (_elsoSzalMegallit)
                {
                    Console.WriteLine("Első szál leállítva.");
                    return; // Szál befejezése
                }

                Console.WriteLine("Első szál: " + i);
                Thread.Sleep(200);
            }
        }

        static void FelfuggesztoMetodus()
        {
            Console.WriteLine("Második szál elindult.");

            for (int i = 0; i < 10; i++)
            {
                if (_masodikSzalMegallit)
                {
                    Console.WriteLine("Második szál felfüggesztve (várakozás).");
                    while (_masodikSzalMegallit) { Thread.Sleep(100); } // Aktív várakozás
                    Console.WriteLine("Második szál folytatódik.");

                }

                Console.WriteLine("Második szál: " + i);
                Thread.Sleep(200);
            }
        }
    }
}
