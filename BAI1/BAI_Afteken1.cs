using System;
using System.Collections.Generic;
using System.Linq;


namespace BAI
{
    public class BAI_Afteken1
    {
        private const string BASE27CIJFERS = "-ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // ***************
        // * OPGAVE 1a/b *
        // ***************
        public static UInt64 Opg1aDecodeBase27(string base27getal)
        {
            // *** IMPLEMENTATION HERE *** //
            //  base27 -> base10:  som(karakterWaarde * factor)     |   factor = 27^machtsfactor gebaseerd op karakterpositie *van rechts naar links startend bij 0*

            
            ulong som = 0; // uiteindelijk gedecodeerde waarde
            ulong factor = 1; // begint met 27^0=1 (dan 27^1=27  etc)

            for (int i = base27getal.Length - 1; i >= 0; i--)//loop per karakter door base27 getal. en we beginnen rechts
            {
                //karakter waarde krijgen
                char c = base27getal[i];
                int karakterWaarde = BASE27CIJFERS.IndexOf(c);

                som += (ulong)karakterWaarde * factor;
                factor *= 27; // volgend karakter = volgende macht 
            }

            return som;
        }
        public static string Opg1bEncodeBase27(UInt64 base10getal)
        {
            // *** IMPLEMENTATION HERE *** //
            // base10 -> base27: 
            if (base10getal == 0)
            {
                return BASE27CIJFERS[0].ToString(); // "-" staat voor 0
            }

            string resultaat = ""; //uiteindelijk encoded getal

            while (base10getal > 0)
            {
                ulong rest = base10getal % 27;               // rest berekenen met modulo
                resultaat = BASE27CIJFERS[(int)rest] + resultaat; // rest omzetten naar base27 karakter en toevoegen aan resultaat
                base10getal /= 27;                           // ga verder met quotiënt
            }

            return resultaat;
        }

        // ***************
        // * OPGAVE 2a/b *
        // ***************
        public static Stack<UInt64> Opdr2aWoordStack(List<string> woorden)
        {
            // *** IMPLEMENTATION HERE *** //
            Stack<UInt64> stack = new Stack<UInt64>();

            foreach (string woord in woorden)
            {
                //decoderen en pushen naar stack
                UInt64 waarde = Opg1aDecodeBase27(woord); 
                stack.Push(waarde); 
            }

            return stack;
        }
        public static Queue<string> Opdr2bKorteWoordenQueue(Stack<UInt64> woordstack)
        {
            // *** IMPLEMENTATION HERE *** //
            Queue<string> queue = new Queue<string>();

            while (woordstack.Count > 0)
            {
                // van stack weghalen en converteer naar base27
                UInt64 waarde = woordstack.Pop();

                // alleen in que als het een klein woord is. Alle getallen < 27^3(19683) hebben 3 letters.
                if (waarde < 19683)
                {
                    // kleine woorden encoderen
                    string woord = Opg1bEncodeBase27(waarde);
                    queue.Enqueue(woord);
                }
                   


            }

            return queue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("=== Opdracht 1a : Decode base-27 ===");
            Console.WriteLine($"BAI    => {Opg1aDecodeBase27("BAI")}");         // 1494
            Console.WriteLine($"HBO-ICT => {Opg1aDecodeBase27("HBO-ICT")}");    // 3136040003
            Console.WriteLine($"BINGO  => {Opg1aDecodeBase27("BINGO")}");       // 1250439
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("=== Opdracht 1b : Encode base-27 ===");
            Console.WriteLine($"1494       => {Opg1bEncodeBase27(1494)}");          // "BAI"
            Console.WriteLine($"3136040003 => {Opg1bEncodeBase27(3136040003)}");    // "HBO-ICT"
            Console.WriteLine($"1250439   => {Opg1bEncodeBase27(1250439)}");        // BINGO
            Console.WriteLine();

            Console.WriteLine("=== Opdracht 2 : Stack / Queue - korte woorden ===");
            string[] woordarray = {"CONCEPT", "OK", "BLAUW", "TOEN", "IS",
                "HBOICT", "GEEL", "DIT", "GOED", "NIEUW"};
            List<string> woorden = new List<string>(woordarray);
            Stack<UInt64> stack = Opdr2aWoordStack(woorden);
            Queue<string> queue = Opdr2bKorteWoordenQueue(stack);

            foreach (string kortwoord in queue)
            {
                Console.Write(kortwoord + " ");                             // Zou "DIT IS OK" moeten opleveren
            }
            Console.WriteLine();
        }
    }
}
