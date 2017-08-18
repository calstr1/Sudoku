using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Game
    {
        public static Board Unsolved = new Board();
        public static void Play()
        {
            while (Unsolved.board != Logic.Solved.board)
            {
                Unsolved.PrintBoard();
                int index = GetIndex();//gets a board array index for the selected box
                Console.WriteLine("Enter a value for this box (1-9): ");
                int value = GetOneNine();//gets a value for the box
                Console.WriteLine(Legallity(index, value));
            }
            Unsolved.PrintBoard();
        }

        public static string Legallity(int index, int value)//Checks whether the value is correct and if so implements
        {
            if (Unsolved.initialBoard[index] != 0)
            {
                if (Unsolved.zeroes.Contains(index))
                {
                    if(Logic.PossList(index, Unsolved).Contains(value))
                    {
                        if (value == Logic.Solved.board[index])
                        {
                            Unsolved.Reconstruct(index, value);
                            Unsolved.PrintBoard();
                            return "That value is correct";
                        }
                        else return "" + value + " is not the correct value.";
                    }
                    else return "There is a " + value + " in the same square, column, or row.";
                }
                else return "That box int empty";
            }
            else return "Original numbers cant be changed.";
        }

        public static int GetIndex()//converts a set of coordinates to a board index
        {
            int[] coords = new int[2];
            Console.WriteLine("Which column (1-9)?");
            coords[0] = GetOneNine()-1;
            Console.WriteLine("Which row (1-9)?");
            coords[1] = GetOneNine()-1;
            return coords[1]*9 + coords[0];
        }

        public static int GetOneNine()//runs until a number between 1 and 9 is given in the correct format
        {
            string input = Console.ReadLine();
            while (!new List<string>(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" }).Contains(input)){
                Console.WriteLine("Please enter a number between 1 and 9: ");
                input = Console.ReadLine();
            }
            return Convert.ToInt32(input);
        }
    }
}
