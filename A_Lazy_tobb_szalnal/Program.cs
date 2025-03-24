using System;
using System.Threading;

namespace A_Lazy_tobb_szalnal
{
    public class KoltsegesOsztaly
    {
        public KoltsegesOsztaly(int szam)
        {
            Console.WriteLine($"Koltseges objektum létrehozva! ({szam})");
        }

        public void Metodus(string szalneve)
        {
            Console.WriteLine($"Koltseges objektum végzi a munkát. ({szalneve})");
        }
    }

    public class Program
    {

        public static void Main()
        {
            //LazyThreadSafetyMode.None - nem szalbiztos

            //LazyThreadSafetyMode.PublicationOnly
            //inicializalas egyszerre csak egy szalon tortenik, de a kesobbi hozzaferes szalbiztos

            //LazyThreadSafetyMode.ExecutionAndPublication
            //inicializalas es a kesobbi hozzaferes is szalbiztos

            //Lazy peldany tobb szalhoz is thread-safe modon fer hozza
            //true - szalbiztos inicializalas, LazyThreadSafetyMode.ExecutionAndPublication
            //Lazy<KoltsegesOsztaly> lustaObjektum = new Lazy<KoltsegesOsztaly>(() => new KoltsegesOsztaly(42), true);
            Lazy<KoltsegesOsztaly> lustaObjektum = new Lazy<KoltsegesOsztaly>(() => new KoltsegesOsztaly(42),
                                                                    LazyThreadSafetyMode.ExecutionAndPublication);


            //Inditunk par szalat, hogy teszteljuk a tobb szalas mukodest
            Thread elsoSzal = new Thread(() => {
                Console.WriteLine("1. szál elindult");
                lustaObjektum.Value.Metodus("1. szál");
            });

            Thread masodikSzal = new Thread(() => {
                Console.WriteLine("2. szál elindult");
                lustaObjektum.Value.Metodus("2. szál");
            });

            elsoSzal.Start();
            masodikSzal.Start();

            elsoSzal.Join();
            masodikSzal.Join();
        }
    }
}
