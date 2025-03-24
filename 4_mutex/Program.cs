using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _mutex
{
    class Test
    {
        private Mutex mutex = new Mutex();
        public void ResourceMetod()
        {
            mutex.WaitOne();
            Console.WriteLine("{0} használja az eroforrást...",
            Thread.CurrentThread.Name);

            Thread.Sleep(200);

            mutex.ReleaseMutex();
            Console.WriteLine("{0} elengedi az erőforrást..."
            , Thread.CurrentThread.Name);
        }
    }


    class Program
    {
        static Test t = new Test();

        static public void ResourceUserMethod()
        {
            for (int i = 0; i < 10; ++i)
            {
                t.ResourceMetod();
            }
        }

        static void Main(string[] args)
        {
            List<Thread> szalLista = new List<Thread>();

            for (int i = 0; i < 10; ++i)
            {
                szalLista.Add(new Thread(new ThreadStart(Program.ResourceUserMethod))
                {
                    Name = (i+1).ToString() + ". szál"
                });
            }
            szalLista.ForEach((thread) => thread.Start());
        }
    }
}
