/*
Unstarted
szál (Szamolas)
Running
Thread was being aborted.
Aborted
új indul
szál (Szamolas)
Running
SuspendRequested, WaitSleepJoin
felfüggesztve
WaitSleepJoin
True
False
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0_allapotok
{
    class Program
    {

        static void Szamolas()
        {

            try
            {
                Console.WriteLine("szál (Szamolas)");
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message); ;
            }

        }

        //Abort, Suspend, Resum - mar nem hasznalando
        static void Main(string[] args)
        {

            ThreadStart st = new ThreadStart(Szamolas);
            Thread ujSzal = new Thread(st);
            ujSzal.Name = "szál neve";
            ujSzal.Priority = ThreadPriority.Normal;

            Console.WriteLine(ujSzal.ThreadState);
            ujSzal.Start();
            Console.WriteLine(ujSzal.ThreadState);
            Thread.Sleep(10);
            ujSzal.Abort();
            Console.WriteLine(ujSzal.ThreadState);

            Console.WriteLine("új indul");
            ujSzal = new Thread(st);
            ujSzal.Start();
            Console.WriteLine(ujSzal.ThreadState);

            ujSzal.Suspend();
            Console.WriteLine(ujSzal.ThreadState);
            Console.WriteLine("felfüggesztve");
            ujSzal.Resume();
            Console.WriteLine(ujSzal.ThreadState);

            Console.WriteLine(ujSzal.IsAlive);
            ujSzal.Join();
            Console.WriteLine(ujSzal.IsAlive);
        }
    }
}
