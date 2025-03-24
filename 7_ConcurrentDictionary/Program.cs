using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _7_ConcurrentDictionary
{
    class Program
    {
        static Dictionary<string, int> szotar = new Dictionary<string, int>();
        //concurrent - egyidejű, párhuzamos, együttműködő, (versenytárs)
        //párhuzamos szótár, de van ConcurrentStack<T>, ConcurrentQueue<T>, ConcurrentBag<T>, BlockingCollection<T> is
        static ConcurrentDictionary<string, int> concurrentSzotar = new ConcurrentDictionary<string, int>();

        static void Main(string[] args)
        {
            Thread szal1 = new Thread(new ThreadStart(Metodus));
            Thread szal2 = new Thread(new ThreadStart(Metodus));
            szal1.Start(); szal2.Start();
            szal1.Join(); szal2.Join();
            Console.WriteLine("Átlag: {0}", szotar.Values.Average());

            szal1 = new Thread(new ThreadStart(Metodus2));
            szal2 = new Thread(new ThreadStart(Metodus2));
            szal1.Start(); szal2.Start();
            szal1.Join(); szal2.Join();
            Console.WriteLine("Átlag: {0}", concurrentSzotar.Values.Average());

            //TryAdd, TryGetValue, TryRemove, TryUpdate

            //TryAdd megpróbálja hozzáadni
            concurrentSzotar.TryAdd("első", 1);
            concurrentSzotar.TryAdd("első", 2);
            concurrentSzotar.TryAdd("második", 2);

            //TryGetValue
            int elso;
            concurrentSzotar.TryGetValue("első", out elso);
            Console.WriteLine(elso);//1
            concurrentSzotar.TryGetValue("harmadik", out elso);
            Console.WriteLine(elso);//0

            //TryRemove
            concurrentSzotar.TryRemove("harmadik", out elso);
            Console.WriteLine(elso);//0
            concurrentSzotar.TryRemove("első", out elso);
            Console.WriteLine(elso);//1
            concurrentSzotar.TryAdd("első", 1);

            //TryUpdate megpróbálja frissíteni
            //frissíti, ha az eredeti érték 4
            concurrentSzotar.TryUpdate("első", 200, 4);

            //frissíti, ha az eredeti érték 1
            concurrentSzotar.TryUpdate("első", 100, 1);

            Console.WriteLine(concurrentSzotar["első"]);
            //eredmény: 100

            //AddOrUpdate, GetOrAdd

            //ha nincs második, akkor hozzáad 5-öt
            //különben megnöveli az értékét eggyel
            concurrentSzotar.AddOrUpdate("második", 5, (k, v) => v + 1);

            Console.WriteLine(concurrentSzotar["második"]);//3

            //lekérdezi harmadik-at, vagy 4-et ad hozzá
            int harmadik = concurrentSzotar.GetOrAdd("harmadik", 43);
            Console.WriteLine(harmadik);//43

            //lekérdezi harmadik-at, vagy 42-őt ad hozzá
            harmadik = concurrentSzotar.GetOrAdd("harmadik", 42);
            Console.WriteLine(harmadik);//43

            //var lista = new List<KeyValuePair<string, int>>();
            //a lista KeyValuePair struktúra tömb lesz
            var lista = concurrentSzotar.ToArray();
            foreach (var elem in lista)
            {
                Console.WriteLine(elem);
            }

            //IsEmpty, Count == 0
            Console.WriteLine(concurrentSzotar.IsEmpty);//false
        }

        static void Metodus()
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    szotar.Add(i.ToString(), i);
                }
                catch (ArgumentException)
                {

                    Console.WriteLine("{0}. kulcs már szerepel a szótárban. ", i);
                }
            }
        }

        static void Metodus2()
        {
            for (int i = 0; i < 100; i++)
            {
                concurrentSzotar.TryAdd(i.ToString(), i);
            }
        }
    }
}
