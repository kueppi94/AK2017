using System;
using System.IO;
using System.Linq;

namespace AK04
{
    class Program
    {
        private static string path = "C://Users//Daniel//Desktop//AK04//AK04//Input.txt";
        private static string[] zeilen;        

        static void Main(string[] args)
        {
            zeilen = File.ReadAllLines(path);
            calculateDuplicatesAnagrams();
            Console.Read();
        }   

        public static void calculateDuplicatesAnagrams()
        {
            int anzOhneDup = 0;
            int anzOhneAna = 0;
            foreach(string zeile in zeilen)
            {
                if (IsNoDuplicate(zeile))
                {
                    anzOhneDup++;
                    if (IsNoAnagram(zeile))
                        anzOhneAna++;
                }
            }

            Console.WriteLine("Loesung Aufg. 1: " + anzOhneDup);
            Console.WriteLine("Loesung Aufg. 2: " + anzOhneAna);
        }
        
        public static bool IsNoDuplicate(string zeile)
        {            
            string[] woerter = zeile.Split(' ');
            //i bestimmt das aktuelle Wort                
            for (int i = 0; i < woerter.Length - 1; i++)
            {
                string aWort = woerter[i];
                //j bestimmt das Vergleichswort
                for (int j = i + 1; j < woerter.Length; j++)
                {
                    string vWort = woerter[j];

                    if (aWort.Equals(vWort))
                        return false;

                }
            }
            return true;
        }  

        public static bool IsNoAnagram(string zeile)
        {
            
            string[] woerter = zeile.Split(' ');
            //i bestimmt das aktuelle Wort                
            for (int i = 0; i < woerter.Length - 1; i++)
            {
                string aWort = woerter[i];
                aWort = String.Concat(aWort.OrderBy(c => c));
                //j bestimmt das Vergleichswort
                for (int j = i + 1; j < woerter.Length; j++)
                {
                    string vWort = woerter[j];                    
                    vWort = String.Concat(vWort.OrderBy(c => c));

                    if (aWort.Equals(vWort))
                        return false;

                }
            }
            return true;            
        }        
    }
}
