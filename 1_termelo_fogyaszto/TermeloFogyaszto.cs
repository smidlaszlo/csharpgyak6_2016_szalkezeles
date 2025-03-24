using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;

//Monitor osztaly
namespace _2TermeloFOgyaszto
{
    class TermeloFogyaszto
    {
        static readonly int gyartandoTermelonkent = 200;
        static readonly int termeloDb = 3;
        static readonly int fogyasztoDb = 2;
        static readonly int gyartandoDb = gyartandoTermelonkent * termeloDb;
        static readonly int taroloKapacitasa = 30;

        static ArrayList puffer = new ArrayList(taroloKapacitasa);
        static int db = 0, jodb = 0;
        static Random rnd = new Random();

        static ArrayList Eloallit(int x)
        {
            ArrayList lista = new ArrayList();
            lista.Add(x);
            while (x != 1)
            {
                for (int p = 2; p <= x; )
                {
                    if (x % p == 0)
                    {
                        lista.Add(p);
                        x = x / p;
                    }
                    else
                        p++;
                }
            }
            return lista;
        }

        static void Termelo()
        {
            for (int i = 0; i < gyartandoTermelonkent; i++)
            {
                int szam;
                lock (rnd)
                    szam = rnd.Next(10000, 90000);
                ArrayList felbontott = Eloallit(szam);
                Hozzaad(felbontott);
            }
        }

        static void Hozzaad(ArrayList x)
        {
            Monitor.Enter(puffer);
            while (puffer.Count >= taroloKapacitasa)
            {
                Monitor.Wait(puffer);
            }

            puffer.Add(x);

            Monitor.Pulse(puffer);
            Monitor.Exit(puffer);
        }

        static bool RendbenVan(ArrayList A)
        {
            int ossz = 1;
            for (int i = 1; i < A.Count; i++)
                ossz *= (int)A[i];
            return (ossz == (int)A[0]);
        }

        static void Fogyaszto()
        {
            while (db < gyartandoDb)
            {
                Monitor.Enter(puffer);

                if (puffer.Count == 0)
                    Monitor.Wait(puffer, 500);

                if (puffer.Count > 0)
                {
                    ArrayList A = puffer[0] as ArrayList;
                    puffer.RemoveAt(0);
                    db++;

                    Monitor.Pulse(puffer);
                    Monitor.Exit(puffer);
                    Console.WriteLine("{0}. db - thread id: {1}", db, Thread.CurrentThread.ManagedThreadId);
                    if (!RendbenVan(A))
                        Console.WriteLine("HIBA");
                    else
                        jodb++;
                }
                else
                {
                    Monitor.Pulse(puffer);
                    Monitor.Exit(puffer);
                }
            }
        }

        static void Main(string[] args)
        {
            var stopper = Stopwatch.StartNew();

            ArrayList szalak = new ArrayList();

            ThreadStart termeloiSzal = new ThreadStart(Termelo);
            for (int i = 0; i < termeloDb; i++)
            {
                Thread szal = new Thread(termeloiSzal);
                szal.Start();
                szalak.Add(szal);
            }

            ThreadStart fogyasztoiSzal = new ThreadStart(Fogyaszto);
            for (int i = 0; i < fogyasztoDb; i++)
            {
                Thread szal = new Thread(fogyasztoiSzal);
                szal.Start();
                szalak.Add(szal);
            }

            foreach (Thread szal in szalak)
                szal.Join();

            Console.WriteLine("A(z) {0} szál megállt! puffer kapacitása: {1}, jó db: {2}", szalak.Count, puffer.Count, jodb);
            //Console.ReadLine();

            stopper.Stop();
            Console.WriteLine("Eltelt idő: " + stopper.Elapsed.TotalMilliseconds + " ms");

        }
    }
}
