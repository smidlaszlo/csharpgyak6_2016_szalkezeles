using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace B_Parallel_peldak
{
    class Program
    {
        public static void RovidMetodus()
        {
            Console.WriteLine($"RovidMetodus() fut a(z) {Thread.CurrentThread.ManagedThreadId} szálon.");
            // Hosszú ideig tartó művelet szimulálása.
            Thread.Sleep(2000);
            Console.WriteLine("RovidMetodus() befejeződött.");
        }

        public static void HosszuMetodus()
        {
            Console.WriteLine($"HosszuMetodus() fut a(z) {Thread.CurrentThread.ManagedThreadId} szálon.");
            // Hosszú ideig tartó művelet szimulálása.
            Thread.Sleep(3000);
            Console.WriteLine("HosszuMetodus() befejeződött.");
        }

        static void Main(string[] args)
        {
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"A(z) {i} feldolgozása. Task egyedi azonosítója: {Task.CurrentId}");
                //Console.WriteLine($"A(z) {i}. iteráció fut a(z) {Thread.CurrentThread.ManagedThreadId} szálon.");
                // Hosszú ideig tartó művelet szimulálása.
                Thread.Sleep(1000);
            });

            List<int> szamok = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Parallel.ForEach(szamok, szam =>
            {
                Console.WriteLine($"A {szam} feldolgozása a(z) {Thread.CurrentThread.ManagedThreadId} szálon.");
                // Hosszú ideig tartó művelet szimulálása.
                Thread.Sleep(1000);
            });

            Console.WriteLine("Párhuzamos ciklusok befejeződtek.");

            Parallel.Invoke(RovidMetodus, HosszuMetodus);

            Console.WriteLine("Párhuzamos metódusok befejeződtek.");

            Parallel.Invoke(
                () => Console.WriteLine("1. feladat"),
                () => Console.WriteLine("2. feladat"),
                () => Console.WriteLine("3. feladat")
            );

            //Több feladat végrehajtása esetén, a Parallel.Invoke hatékonyabb lehet, 
            //mint a Task.WhenAll vagy a manuálisan létrehozott szálak, 
            //mivel az automatikusan optimalizálja a párhuzamos végrehajtást.
        }
    }
}
