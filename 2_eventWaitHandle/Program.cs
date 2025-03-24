using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2_eventWaitHandle
{
    class Program
    {
        static EventWaitHandle egyik = new AutoResetEvent(false);
        static EventWaitHandle masik = new AutoResetEvent(false);
        static readonly object _locker = new object();
        static string uzenet;

        static void Main()
        {
            new Thread(Metodus).Start();

            egyik.WaitOne();
            lock (_locker) uzenet = "első szöveg";
            masik.Set();

            egyik.WaitOne();
            lock (_locker) uzenet = "második szöveg";
            masik.Set();
            egyik.WaitOne();
            lock (_locker) uzenet = null;
            masik.Set();
        }

        static void Metodus()
        {
            while (true)
            {
                egyik.Set();
                masik.WaitOne();
                lock (_locker)
                {
                    if (uzenet == null) return;
                    Console.WriteLine(uzenet);
                }
            }
        }
    }
}
