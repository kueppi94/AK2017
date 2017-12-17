using System;
using System.Collections.Generic;

namespace AK17
{
    class Program
    {
        private static int stepSize = 394;  
              
        static void Main(string[] args)
        {
            Console.WriteLine("Value after 2017: " + GetValueAfter2017(stepSize));
            Console.WriteLine("Value after 0 (50e6 runs): " + GetValueAfter0(stepSize));
            Console.Read();
        }

        public static int GetValueAfter2017(int stepSize)
        {
            List<int> buffer = new List<int>();
            int insertPos = 0;
            for(int i = 0; i < 2017; i++)
            {
                buffer.Insert(insertPos, i);
                insertPos = (insertPos + stepSize) % buffer.Count +1;               
            }
            
            return buffer[insertPos];
        }

        public static int GetValueAfter0(int stepSize)
        {           
            int output = -1;         
            int insertPos = 0;        
            for(int i = 1; i <= 50e6; i++)
            {
                insertPos = (insertPos + stepSize) % i + 1;
                if (insertPos == 1)
                    output = i;
            }
            
            return output;
        }
    }
}
