using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _8_ConcurrentStack
{
    class Program
    {
        static void Main(string[] args)
        {

            ConcurrentStack<int> concurrentVerem = new ConcurrentStack<int>();

            //verembe érték betétele
            for (int i = 0; i < 5000; i++)
            {
                concurrentVerem.Push(i);
            }


            int szamlalo = 0;

            //taszk létrehozása és indítása
            Task[] veremTaszkok = new Task[10];
            for (int i = 0; i < veremTaszkok.Length; i++)
            {
                veremTaszkok[i] = Task.Factory.StartNew(() =>
                {
                    while (concurrentVerem.Count > 0)
                    {
                        int aktualisElem;
                        //veremből érték kivételének próbája
                        bool sikeres = concurrentVerem.TryPop(out aktualisElem);
                        if (sikeres)
                        {
                            //sikeres kivétel esetén a számláló növelése
                            Interlocked.Increment(ref szamlalo);
                        }
                    }
                });
            }

            Task.WaitAll(veremTaszkok);
            Console.WriteLine("Számláló: {0}", szamlalo);
        }
    }
}
