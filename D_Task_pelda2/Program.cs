using System;
using System.Threading;
using System.Threading.Tasks;

namespace D_Task_pelda2
{
    class Program
    {
        public static void Metodus()
        {
            Console.WriteLine("A Metodus() fut.");
            // Hosszú ideig tartó művelet szimulálása.
            Thread.Sleep(2000);
            Console.WriteLine("A Metodus() befejeződött.");
        }

        public static int Lekerdezes()
        {
            Console.WriteLine("A Lekerdezes() fut.");
            // Hosszú ideig tartó művelet szimulálása.
            Thread.Sleep(2000);
            Console.WriteLine("A Lekerdezes() befejeződött.");
            return 42;
        }

        public static async Task<double> AszinkronLekerdezes()
        {
            Console.WriteLine("Az AszinkronLekerdezes() fut.");
            await Task.Delay(2000); // Aszinkron késleltetés
            Console.WriteLine("Az AszinkronLekerdezes() befejeződött.");
            return 3.14;
        }

        static void Main(string[] args)
        {
            Program.Indit();

            Console.WriteLine("Fő szál befejezve.");
            Console.ReadKey();
        }

        static async void Indit()
        {
            Console.WriteLine("Fő szál indítása.");

            // Metódus futtatása új szálon.
            Task.Run((Action)Metodus);
            //Task.Run(() => Metodus());

            // Lekérdezés futtatása új szálon és a visszatérési érték lekérése.
            Task<int> lekerdezesiFeladat = Task.Run((Func<int>)Lekerdezes);

            // A task.Result blokkolja a fő szálat.
            int lekerdezesEredmenye = lekerdezesiFeladat.Result;

            Console.WriteLine($"A Lekerdezes() visszatérési értéke: {lekerdezesEredmenye}");

            // AszinkronLekerdezes() futtatása új szálon lambda kifejezéssel, és a visszatérési érték lekérése.
            Task<double> aszinkronFeladat = Task.Run(() => AszinkronLekerdezes());

            // A visszatérési érték lekérése az await kulcsszóval.
            double aszinkronEredmeny = await aszinkronFeladat;

            Console.WriteLine($"Az AszinkronLekerdezes() visszatérési értéke: {aszinkronEredmeny}");
        }
    }
}
