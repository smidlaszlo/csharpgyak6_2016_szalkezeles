using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _9_ConcurrentStack2
{
    class Program
    {
        //Push();
        //TryPeek();
        //TryPop();
        //Clear();
        //IsEmpty;
        //PushRange();
        //TryPopRange();

        static void Main()
        {
            int hibaDb = 0;
            int eredmeny;

            //ConcurrentStack létrehozása
            ConcurrentStack<int> concurrentVerem = new ConcurrentStack<int>();

            //elemek verembe helyezése
            concurrentVerem.Push(1);
            concurrentVerem.Push(2);


            //a verem tetején lévő elem megnézése
            if (!concurrentVerem.TryPeek(out eredmeny))
            {
                Console.WriteLine("TryPeek() hiba");
                hibaDb++;
            }
            else if (eredmeny != 2)
            {
                Console.WriteLine("TryPeek() 2 helyett {0}-t kapott", eredmeny);
                hibaDb++;
            }

            //a verem tetején lévő elem kivétele
            if (!concurrentVerem.TryPop(out eredmeny))
            {
                Console.WriteLine("TryPop() hiba");
                hibaDb++;
            }
            else if (eredmeny != 2)
            {
                Console.WriteLine("TryPop() 2 helyett {0}-t kapott", eredmeny);
                hibaDb++;
            }

            //a verem kitisztítása
            concurrentVerem.Clear();

            //ellenőrzés, hogy üres-e a verem
            if (!concurrentVerem.IsEmpty)
            {
                Console.WriteLine("IsEmpty nem igaz Clear() után");
                hibaDb++;
            }


            concurrentVerem = new ConcurrentStack<int>();

            //egymást követő számok elhelyezése a veremben tartománnyal
            concurrentVerem.PushRange(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            concurrentVerem.PushRange(new int[] { 8, 9, 10 });
            concurrentVerem.PushRange(new int[] { 11, 12, 13, 14 });
            concurrentVerem.PushRange(new int[] { 15, 16, 17, 18, 19, 20 });
            concurrentVerem.PushRange(new int[] { 21, 22 });
            concurrentVerem.PushRange(new int[] { 23, 24, 25, 26, 27, 28, 29, 30 });

            //egyidejűleg 3 értéket olvasunk vissza
            Parallel.For(0, 10, i =>
            {
                int[] tartomany = new int[3];
                if (concurrentVerem.TryPopRange(tartomany) != 3)
                {
                    Console.WriteLine("TryPopRange() hiba");
                    Interlocked.Increment(ref hibaDb);
                }

                //fordított sorrendet kellene kapnunk
                if (!tartomany.Skip(1).SequenceEqual(tartomany.Take(tartomany.Length - 1).Select(x => x - 1)))
                {
                    Console.WriteLine("Egymást követő számokat vártunk.  tartomany[0]={0}, tartomany[1]={1}",
                                    tartomany[0], tartomany[1]);
                    Interlocked.Increment(ref hibaDb);
                }
            });

            //a veremnek üresnek kellene lennie
            if (!concurrentVerem.IsEmpty)
            {
                Console.WriteLine("IsEmpty-nek igaznak kellene lennie");
                hibaDb++;
            }

            if (hibaDb == 0) Console.WriteLine("Nem volt hiba!");


        }
    }
}
