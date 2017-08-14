using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Sudoku;

namespace Sudoku
{
    class Board
    {
        public static int[,] rows = new int[9, 9];
        public static int[,] columns = new int[9, 9];
        public static int[,] squares = new int[9, 9];
        public static int[] board = new int[81];
        
        public static void Fill(int[] input)//fills out the board
        {
            board = input;
            int num1 = input[0];
            if (num1 == 0) Game.zeroes.Add(0);
            columns[0, 0] = input[0];
            rows[0, 0] = input[0];
            squares[0, 0] = input[0];
            for (int i = 1; i < 81; i++)
            {
                int num = input[i];
                if (num == 0) Game.zeroes.Add(i);
                int row = i / 9;
                int col = i % 9;
                int squ = (3 * (row / 3)) + (col / 3);
                columns[col, row] = num; //swap these 3 lines for a function
                rows[row, col] = num;
                squares[squ, col % 3 + (3 * (row % 3))] = num;

            }
        }

        public static void Reconstruct(int i, int num)//finalises changes and updates board
        {
            int row = i / 9;
            int col = i % 9;
            int squ = (3 * (row / 3)) + (col / 3);
            columns[col, row] = num;
            rows[row, col] = num;
            squares[squ, col % 3 + (3 * (row % 3))] = num;
        }

        public static void PrintBoard()//outputs the board in its current state
        {
            for (int i = 0; i < 9; i++)
            {
                String line = "";
                for (int j = 0; j < 9; j++)
                {
                    String num = rows[i,j].ToString();
                    if (num == "") num = " ";
                    line += num + " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }

    public static class ArrayExt
    {
        public static T[] GetArr<T>(this T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];
            int size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }
    }
}
