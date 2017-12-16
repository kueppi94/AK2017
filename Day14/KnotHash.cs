using System.Collections.Generic;
using System.Linq;


namespace AK14
{
    class KnotHash
    {
        private const int SIZE = 256;

        public static string GetKnotHash(string input)
        {
            int[] denseHash = GetDenseHash(input);
            

            string knothash = string.Join(string.Empty, denseHash.Select(x => ConvertToHex(x)));
            return knothash;
        }

        public static int[] GetDenseHash(string input)
        {
            int[] sparseHash = GetSparseHash(input);
            int[] denseHash = GetDenseHash(sparseHash);

            return denseHash;
        }

        public static int[,] GetDenseHashBinary(string key)
        {
            int[,] denseHashes = new int[128, 128];
            
            for (int i = 0; i < denseHashes.GetLength(0); i++)
            {
                int[] curHash = GetDenseHash(key + "-" + i);                
                int curPos = 0;

                for (int j = 0; j < curHash.Length; j++)
                {                    
                    for (int k = 7; k >= 0; k--)
                    {
                        denseHashes[i, curPos] = (curHash[j] >> k) & 1;                        
                        curPos++;
                    }
                }               
            }

            return denseHashes;
        }

        private static int[] GetSparseHash(string input)
        {
            List<int> byteLength = input.Select(x => (int)x).ToList();
            byteLength.AddRange(new[] { 17, 31, 73, 47, 23 });

            int[] circularList = Enumerable.Range(0, SIZE).ToArray();

            int skipsize = 0;
            int pos = 0;
            for (int i = 0; i < 64; i++)
            {
                foreach (int length in byteLength)
                {
                    Reverse(circularList, pos, length);
                    pos = (pos + length + skipsize) % circularList.Length;
                    skipsize++;
                }
            }

            return circularList;
        }

        private static int[] GetDenseHash(int[] sparseHash)
        {
            int[] denseHash = new int[16];
            int k = 0;
            for (int i = 0; i < sparseHash.Length; i += 16)
            {
                for (int j = i; j < i + 16; j++)
                {
                    denseHash[k] ^= sparseHash[j];
                }
                k++;
            }

            return denseHash;
        }

        private static string ConvertToHex(int hash)
        {
            return hash.ToString("x").PadLeft(2, '0').ToUpper();
        }

        public static void Reverse(int[] a, int start, int length)
        {
            int pos1 = start;
            int pos2 = (start + length - 1) % a.Length;

            while (length > 1)
            {
                Swap(a, pos1, pos2);
                length -= 2;
                pos1 = (pos1 + 1) % a.Length;
                pos2 = (((pos2 - 1) % a.Length) + a.Length) % a.Length;
            }
        }

        private static void Swap(int[] circularList, int i1, int i2)
        {
            int tmp = circularList[i1];
            circularList[i1] = circularList[i2];
            circularList[i2] = tmp;
        }
    }
}
