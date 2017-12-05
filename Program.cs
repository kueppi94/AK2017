using System;
using System.IO;


namespace AK05
{
    class Program
    {
        private static int[] jumps;
        private static string path = "C://...";        

        static void Main(string[] args)
        {            
            Console.WriteLine("The exit is reached in " + EscapeMaze(Jump) + " steps.");
            Console.WriteLine("The exit is reached in " + EscapeMaze(DecreasingJump) + " steps.");
            
            Console.Read();
        }

        public static int EscapeMaze(Func<int, int> method)
        {
            string[] input = File.ReadAllLines(path);
            jumps = Array.ConvertAll(input, int.Parse);

            int steps = 0;
            int curPos = 0;

            while(curPos != -1)
            {
                curPos = method(curPos);
                steps++;
            }

            return steps;
        }        

        //Returns new pos. and updates array
        //Return -1 if maze is escaped
        public static int Jump(int curPos)
        {
            int newPos = curPos + jumps[curPos];
            jumps[curPos]++;

            if (newPos >= jumps.Length || newPos < 0)
                return -1;

            return newPos;
        }

        public static int DecreasingJump(int curPos)
        {
            int newPos = curPos + jumps[curPos];

            if (jumps[curPos] < 3)
                jumps[curPos]++;
            else
                jumps[curPos]--;

            if (newPos >= jumps.Length || newPos < 0)
                return -1;

            return newPos;
        }
    }
}
