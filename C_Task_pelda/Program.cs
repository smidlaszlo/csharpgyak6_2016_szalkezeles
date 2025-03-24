#define OSSZESFELADATOTMEGVAR
using System;
using System.Threading;
using System.Threading.Tasks;

namespace C_Task_pelda
{
    class Program
    {
        static void Main(string[] args)
        {
            int elsoFeladatIdeje = 2000;
            int masodikFeladatIdeje = 3000;

            Console.WriteLine("Fő szál indítása.");

            // Feladat futtatása új szálon.
            Task taszk = Task.Run(() =>
            {
                Console.WriteLine("Feladat futtatása új szálon.");
                // Hosszú ideig tartó művelet szimulálása.
                Thread.Sleep(elsoFeladatIdeje);
                Console.WriteLine("Feladat befejezve.");
            });

            Console.WriteLine("Fő szál megvárja a feladat befejezését.");
            taszk.Wait();
            Console.WriteLine("Fő szál folytatása.");


            // Több feladat futtatása új szálon.
            Task elsoTaszk = Task.Run(() =>
            {
                Console.WriteLine("1. feladat futtatása új szálon.");
                // Hosszú ideig tartó művelet szimulálása.
                Thread.Sleep(elsoFeladatIdeje);
                Console.WriteLine("1. feladat befejezve.");
            });

            Task masodikTaszk = Task.Run(() =>
            {
                Console.WriteLine("2. feladat futtatása új szálon.");
                // Hosszú ideig tartó művelet szimulálása.
                Thread.Sleep(masodikFeladatIdeje);
                Console.WriteLine("2. feladat befejezve.");
            });

#if OSSZESFELADATOTMEGVAR
            Console.WriteLine("Fő szál megvárja az összes feladat befejezését.");
            Task.WhenAll(elsoTaszk, masodikTaszk).Wait();
#else
            Console.WriteLine("Fő szál megvárja a rövidebb idejű feladat befejezését.");
            Task.WhenAny(elsoTaszk, masodikTaszk).Wait();
#endif

            Console.WriteLine("Fő szál befejezve.");

            Console.ReadKey();
        }
    }
}
