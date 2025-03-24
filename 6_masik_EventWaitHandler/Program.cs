using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _6_masik_EventWaitHandler
{
    public class MasikEventWaitHandler
    {
        private static EventWaitHandle varakozasKezelo;

        //számláló, hogy megvárjuk, hogy minden szál elindult-e
        //long az Interlocked bemutatása miatt
        private static long szalSzamlalo = 0;

        //a Main szálat fogja blokkolni
        private static EventWaitHandle varakozasKezeloMainMetodusnak =
            new EventWaitHandle(false, EventResetMode.AutoReset);

        public static void Main()
        {
            //AutoReset típusú EventWaitHandle létrehozása
            varakozasKezelo = new EventWaitHandle(false, EventResetMode.AutoReset);

            //5 szál létrehozása
            SzalInditas(5);

            //a szálak elengedése
            while (Interlocked.Read(ref szalSzamlalo) > 0)
            {
                Console.WriteLine("Egy billentyű lenyomása után elindul egy várakozó szál.");
                Console.ReadKey();

                //az 1. paraméter várakozói közül egynek Signal-t küld, és elengedi a blokkolásból,
                //a 2. paraméternek egy WaitOne()-t ad ki, blokkolja, jelen esetben a Main szálat,
                //hogy az elindított szál a SzalMetodus-ban el tudja végezni a többi műveletet
                WaitHandle.SignalAndWait(varakozasKezelo, varakozasKezeloMainMetodusnak);
            }
            Console.WriteLine();

            //ManualReset típusú EventWaitHandle létrehozása
            varakozasKezelo = new EventWaitHandle(false, EventResetMode.ManualReset);

            //5 szál ismételt elindítása
            SzalInditas(5);

            Console.WriteLine("Egy billentyű lenyomása után elindulnak a várakozó szálak.");
            Console.ReadKey();
            //ManualReset esetén a signal küldésére mindegyik várakozó szál elindul
            varakozasKezelo.Set();
        }

        //szálak létrehozása, paraméterátadással
        public static void SzalInditas(int darab)
        {
            for (int i = 0; i < darab; i++)
            {
                Thread szal = new Thread(
                    new ParameterizedThreadStart(SzalMetodus)
                );
                szal.Start(i + 1);
            }

            //az Interlocked osztály biztosítja, hogy a 64 bites értékek
            //jól működjenek 32 bites rendszeren is, a művelet atomi legyen
            //várakozik, hogy a szalSzamlalo darab értéekű legyen, hogy mindegyik szál blokkolódjon
            while (Interlocked.Read(ref szalSzamlalo) < darab)
            {
                Thread.Sleep(500);
            }
        }

        //a szálban ez a metódus indul el
        public static void SzalMetodus(object adat)
        {
            int index = (int)adat;

            Console.WriteLine("{0}. szál blokkolódott.", adat);
            //blokkolt szálak számának növelése
            Interlocked.Increment(ref szalSzamlalo);

            //várakozás kiadása, a szál blokkolódni fog Set, vagy Signal kiadásáig
            varakozasKezelo.WaitOne();

            Console.WriteLine("{0}. szál ismét elindult.", adat);
            //blokkolt szálak számának csökkentése
            Interlocked.Decrement(ref szalSzamlalo);

            //a Main szál elindítása
            varakozasKezeloMainMetodusnak.Set();
        }
    }
}
