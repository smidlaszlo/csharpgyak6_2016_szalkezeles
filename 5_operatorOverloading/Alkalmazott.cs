using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_operatorOverloading
{
    class Alkalmazott
    {
        public string Nev { get; set; }
        public long Fizetes { get; set; }

        public Alkalmazott(string nev, long fizetes)
        {
            Nev = nev;
            Fizetes = fizetes;
        }

        //konvenció lhs (left hand side), rhs (right hand side)
        public static bool operator==(Alkalmazott balertek, Alkalmazott jobbertek)
        {
            return balertek.Fizetes == jobbertek.Fizetes;
        }

        public static bool operator!=(Alkalmazott balertek, Alkalmazott jobbertek)
        {
            //return balertek.Fizetes != jobbertek.Fizetes;
            return !(balertek == jobbertek);
        }

        public static Alkalmazott operator+(Alkalmazott balertek, Alkalmazott jobbertek)
        {
            return new Alkalmazott("uj", balertek.Fizetes + jobbertek.Fizetes);
        }

        static public Alkalmazott operator ++(Alkalmazott jobbertek)
        {
            ++jobbertek.Fizetes;
            return jobbertek;
        }

        static public bool operator <(Alkalmazott balertek, Alkalmazott jobbertek)
        {
            return balertek.Fizetes< jobbertek.Fizetes;
        }

        static public bool operator >(Alkalmazott balertek, Alkalmazott jobbertek)
        {
            return balertek.Fizetes > jobbertek.Fizetes;
        }


        public override string ToString()
        {
            return Nev + ", " + Fizetes;
        }

        //szűkebbről tágabbra konverziókat implicit módon
        //vagyis jelölés nélkül
        public static implicit operator Alkalmazott(long fizetes)
        {
            return new Alkalmazott("uj", fizetes);
        }

        //tágabbról szűkebbre konvertálást explicit módon
        //ezt jelöljük
        public static explicit operator long(Alkalmazott jobb)
        {
            return jobb.Fizetes;
        }

        public static explicit operator Alkalmazott(string jobb)
        {
            return new Alkalmazott("uj", long.Parse(jobb));
        }
    }
}
