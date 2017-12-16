using System;

namespace AK14
{
    class Program
    {
        private static string key = "ffayrhll";        

        static void Main(string[] args)
        {
            Console.WriteLine("Count of used squares: " + GetUsedSquares(key));
            Console.WriteLine("Count of groups: " + GetGroupCount(KnotHash.GetDenseHashBinary(key)));

            Console.Read();
        }

        public static int GetUsedSquares(string key)
        {
            string[] knotHashes = GetKnotHashes(key);
            int count = 0;
            foreach (string hash in knotHashes)
            {
                foreach (char l in hash)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int help = Convert.ToInt32(string.Empty + l, 16);
                        if ((help >> i & 1) == 1)
                            count++;
                    }
                }
            }

            return count;
        }

        public static int GetGroupCount(int[,] denseHash)
        {            
            int[,] copy = denseHash.Clone() as int[,];
            int lastGroup = 0;
            for(int i = 0; i < copy.GetLength(0); i++)
            {
                for(int j = 0; j < copy.GetLength(1); j++)
                {
                    if(copy[i,j] == 1)
                        DetermineGroup(copy, lastGroup--, i, j);
                }
            }
            
            return Math.Abs(lastGroup); 
        }

        private static void DetermineGroup(int[,] denseHash, int lastGroup, int row, int column)
        {
            denseHash[row, column] = lastGroup;
            //below
            if (row + 1 < denseHash.GetLength(0) && denseHash[row + 1, column] == 1)
                DetermineGroup(denseHash, lastGroup, row + 1, column);
            //right
            if (column + 1 < denseHash.GetLength(1) && denseHash[row, column + 1] == 1)
                DetermineGroup(denseHash, lastGroup, row, column + 1);
            //left
            if (column - 1 >= 0 && denseHash[row, column -1] == 1)
                DetermineGroup(denseHash, lastGroup, row, column -1);
            //above
            if (row-1 >= 0 && denseHash[row-1, column] == 1)
                DetermineGroup(denseHash, lastGroup, row -1, column);

        }


        public static string[] GetKnotHashes(string key)
        {
            string[] grid = new string[128];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = KnotHash.GetKnotHash(key + "-" + i);
            }

            return grid;
        }
    }
}
