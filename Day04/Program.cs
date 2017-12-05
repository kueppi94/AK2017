using System;
using System.IO;
using System.Linq;

namespace AK04
{
    class Program
    {
        private static string path = "C://...";
        private static string[] rows;        

        static void Main(string[] args)
        {
            rows = File.ReadAllLines(path);
            CalculateDuplicatesAndAnagrams();
            Console.Read();
        }   

        public static void CalculateDuplicatesAndAnagrams()
        {
            int noDupCount = 0;
            int noAnagrCount = 0;
            foreach(string zeile in rows)
            {
                if (IsNoDuplicate(zeile))
                {
                    noDupCount++;
                    if (IsNoAnagram(zeile))
                        noAnagrCount++;
                }
            }

            Console.WriteLine("Passphrases without duplicate words: " + noDupCount);
            Console.WriteLine("Passphrases without anagrams: " + noAnagrCount);
        }
        
        public static bool IsNoDuplicate(string row)
        {            
            string[] words = row.Split(' ');
            //i describes the current word            
            for (int i = 0; i < words.Length - 1; i++)
            {
                string cWord = words[i];
                //j describes the comparison word
                for (int j = i + 1; j < words.Length; j++)
                {
                    string cmWord = words[j];

                    if (cWord.Equals(cmWord))
                        return false;
                }
            }
            return true;
        }  

        public static bool IsNoAnagram(string row)
        {            
            string[] words = row.Split(' ');
            //i describes the current word                 
            for (int i = 0; i < words.Length - 1; i++)
            {
                string cWord = words[i];
                cWord = String.Concat(cWord.OrderBy(c => c));
                //j describes the comparison word
                for (int j = i + 1; j < words.Length; j++)
                {
                    string cmWord = words[j];                    
                    cmWord = String.Concat(cmWord.OrderBy(c => c));

                    if (cWord.Equals(cmWord))
                        return false;
                }
            }
            return true;            
        }        
    }
}
