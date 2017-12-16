using System;

namespace AK15
{
    class Program
    {
        private static long valueGA = 277L;
        private static long valueGB = 349L;

        static void Main(string[] args)
        {
            Console.WriteLine("Number of matches after 40e6 rounds: " + GetMatches40e6Rounds(valueGA, valueGB));
            Console.WriteLine("Number of matches after 5e6 rounds: " + GetMatches5e6Rounds(valueGA, valueGB));
            Console.Read();
        }

        public static int GetMatches40e6Rounds(long valueGA, long valueGB)
        {
            int matches = 0;
            for (int i = 0; i < 40e6; i++)
            {
                valueGA = GetNextValue(valueGA, true);
                valueGB = GetNextValue(valueGB, false);

                string binaryA = ToBinary(valueGA);
                string binaryB = ToBinary(valueGB);

                if (Match16Bits(binaryA, binaryB))
                    matches++;
            }

            return matches;
        }

        public static int GetMatches5e6Rounds(long valueGA, long valueGB)
        {
            int matches = 0;
            for (int i = 0; i < 5e6; i++)
            {
                do               
                    valueGA = GetNextValue(valueGA, true);
                while (valueGA % 4 != 0);

                do
                    valueGB = GetNextValue(valueGB, false);
                while (valueGB % 8 != 0);

                string binaryA = ToBinary(valueGA);
                string binaryB = ToBinary(valueGB);

                if (Match16Bits(binaryA, binaryB))
                    matches++;
            }

            return matches;
        }



        private static bool Match16Bits(string string1, string string2)
        {     
            string help1 = string1.Substring(string1.Length - 16);
            string help2 = string2.Substring(string2.Length - 16);
            return help1.Equals(help2);
        }

        private static string ToBinary(long value)
        {
            return Convert.ToString(value, 2).PadLeft(64, '0');
        }

        private static long GetNextValue(long prevValue, bool isGeneratorA)
        {
            if (isGeneratorA)
                return (prevValue * 16807L) % 2147483647L;
            return (prevValue * 48271L) % 2147483647L;
        }
    }
}
