using System;
using System.IO;
using System.Linq;


namespace AK19
{
    class Program
    {
        private enum Direction { UP = -1, DOWN = 1, LEFT = -1, RIGHT = 1 };
        private static int steps = 0;

        private static string path = "C://...";
        
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(path);
            char[][] field = input.Select(x => x.ToCharArray()).ToArray(); 
            int[] start =  GetStartPosition(input);       

            Console.WriteLine(GetLetters(field, start));
            Console.Read();
        }

        public static string GetLetters(char[][] field, int[] startPos)
        {
            return MoveUpAndDown(field, startPos[0], startPos[1], Direction.DOWN, "Letters are collected in following order: ");
        }

        private static int[] GetStartPosition(string[] input)
        {
            return new[] { 0, input[0].IndexOf("|") };
        }

        private static string MoveLeftAndRight(char[][] field, int row, int column, Direction curDirection, string output)
        {
            steps++;        
            do
            {
                if (field[row][column] != '|' && field[row][column] != '-')
                    output += field[row][column];

                column += (int)curDirection;
                steps++;
            } while (EndNotReached(field, new[] { row, column }) && field[row][column] != '+');            


            //Checks for new direction
            if (row + 1 < field.Length && field[row + 1][column] != ' ')
                return MoveUpAndDown(field, row +1, column, Direction.DOWN, output);
            else if (row >= 1 && field[row -1][column] != ' ')
                return MoveUpAndDown(field, row - 1, column, Direction.UP, output);
            else
            {
                if (field[row][column] != '|' && field[row][column] != '-')
                    output += field[row][column] + "\nThe package needs " + steps + " steps.";
                return output;
            }
        }

        private static string MoveUpAndDown(char[][] field, int row, int column, Direction curDirection, string output)
        {
            steps++;              
            do
            {
                if (field[row][column] != '|' && field[row][column] != '-')
                    output += field[row][column];

                row += (int)curDirection;
                steps++;
            } while (EndNotReached(field, new[] { row, column }) && field[row][column] != '+') ;


            //Checks for new direction
            if (column >= 1 &&field[row][column -1] != ' ')
                return MoveLeftAndRight(field, row, column -1, Direction.LEFT, output);
            else if (column +1 < field[row].Length && field[row][column +1] != ' ')
                return MoveLeftAndRight(field, row, column +1, Direction.RIGHT, output);
            else
            {
                if (field[row][column] != '|' && field[row][column] != '-')
                    output += field[row][column] + "\nThe package needs " + steps + " steps.";
                return output;
            }
        } 
        
        private static bool EndNotReached(char[][] field, int[] curPos)
        {
            int n = 0;
            int row = curPos[0];
            int column = curPos[1];


            if (row >= 1 && field[row -1][column] != ' ')
                n++;
            if(row +1 < field.Length && field[row + 1][column] != ' ')
                n++;
            if (column >= 1 && field[row][column -1] != ' ')
                n++;
            if (column +1 < field[row].Length && field[row][column +1] != ' ')
                n++;

            return n > 1;
        }       
    }
}
