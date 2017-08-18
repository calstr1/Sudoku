using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;

namespace Sudoku
{
    class Logic
    {
        public static Board Solved = new Board();
        public static void Main(string[] args)
        {
            string line = Console.ReadLine();
            int[] input = line.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            Solved.Fill(input);
            Game.Unsolved.Fill(input);
            Solved.PrintBoard();
            
            while (Solved.zeroes.Count() != 0)
            {
                int initial = Solved.zeroes.Count();
                for (int i = 0; i < Solved.zeroes.Count(); i++)
                {
                    int boardIndex = Solved.zeroes.ElementAt(i);
                    List<int> currPoss = PossList(boardIndex, Solved);
                    if (currPoss.Count() == 1 && Solved.zeroes.Contains(boardIndex))//if there is only one possible number for that square, enter it
                    {
                        int num = currPoss.ElementAt(0);
                        //Solved.board[boardIndex] = num;
                        Solved.Reconstruct(boardIndex, num);
                        Solved.zeroes.Remove(boardIndex);
                    }
                    else
                    {
                        /*return from zero list matching row, column, and square find unique possibility, implrement, repeat for squ and col
                        *In other words, if a row needs 3 numbers, and one of those numbers only has one place it could go,
                        * regardless of whether you record it as the only possible option or not, it must be the solution and is entered
                        */
                        int row = boardIndex / 9;
                        int col = boardIndex % 9;
                        int squ = (3 * (row / 3)) + (col / 3);
                        int[] rowIndx = new int[9];
                        int[] colIndx = new int[9];
                        int[] squIndx = new int[9];
                        for (int a = 0; a < 9; a++)//populates the index lists for the current row and column
                        {
                            rowIndx[a] = (row * 9) + a;
                            colIndx[a] = col + (9 * a);
                        }
                        for (int c = 0; c < 3; c++)//populates the index lists for the current square
                        {
                            for (int d = 0; d < 3; d++)
                            {
                                squIndx[(c * 3) + (d)] = (((squ - squ % 3) + c) * 9) + ((squ % 3) * 3) + d;
                            }
                        }
                        List<int> zRow = new List<int>();//lists that will hold the indexes of all the empty or 'zero' values in the row column or square
                        List<int> zCol = new List<int>();
                        List<int> zSqu = new List<int>();
                        for (int iter = 0; iter < 9; iter++)//populates zero lists
                        {
                            int r1 = rowIndx[iter];
                            int c = colIndx[iter];
                            int s = squIndx[iter];
                            if (Solved.zeroes.Contains(r1)) zRow.Add(r1);
                            if (Solved.zeroes.Contains(c)) zCol.Add(c);
                            if (Solved.zeroes.Contains(s)) zSqu.Add(s);
                        }
                        IndexLogic(zRow);//applies aforementioned logic through the IndexLogic function to the applicable row, column, and square
                        IndexLogic(zCol);
                        IndexLogic(zSqu);
                    }
                }
                if (initial == Solved.zeroes.Count()) { Console.WriteLine("true"); break; }
            }
            Console.WriteLine("Do you wish to solve the puzzle (y/n)?: ");
            if(Console.ReadLine() == "y")
            {
                Game.Play();
            }
            else Solved.PrintBoard();
            

        Console.Read();
        }

        public static List<int> PossList(int i, Board board)//returns a list with all the legal number options
        {
            int row = i / 9;
            int col = i % 9;
            int squ = (3 * (row / 3)) + (col / 3);
            List<int> rcs = new List<int>();
            rcs.AddRange(board.rows.GetArr(row));
            rcs.AddRange(board.columns.GetArr(col));
            rcs.AddRange(board.squares.GetArr(squ));
            List<int> poss = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            poss.RemoveAll(item => rcs.Contains(item));
            return poss;
        }

        public static void IndexLogic(List<int> z)// 'z' is for zero
        {
            List<List<int>> indexesPoss = new List<List<int>>();
            List<int> remove = new List<int>();
            List<int> unique = new List<int>();
            List<int> temp = new List<int>();
            List<int> currPoss = new List<int>();
            List<int> noCheck = new List<int>();
            for (int i = 0; i < z.Count(); i++)
            {
                int boardIndex = z.ElementAt(i);
                currPoss = PossList(boardIndex, Solved);
                if (currPoss.Count() == 1 && Solved.zeroes.Contains(boardIndex))
                {
                    int num = currPoss.ElementAt(0);
                    //Solved.board[boardIndex] = num;
                    Solved.Reconstruct(boardIndex, num);
                    Solved.zeroes.Remove(boardIndex);
                    //printBoard();
                    temp.Add(num);
                    noCheck.Add(indexesPoss.Count());
                    indexesPoss.Add(new List<int>());
                }
                else indexesPoss.Add(currPoss);

            }
            for (int j = 0; j < indexesPoss.Count(); j++)
            {
                if (!noCheck.Contains(j))
                {
                    for (int k = 0; k < indexesPoss.ElementAt(j).Count(); k++)
                    {
                        int val = indexesPoss.ElementAt(j).ElementAt(k);
                        if (!unique.Contains(val))
                        {
                            unique.Add(val);
                        }
                        else if (!temp.Contains(val))
                        {
                            temp.Add(val);
                        }
                    }
                }
            }
            unique.RemoveAll(item => temp.Contains(item));
            for (int j = 0; j < indexesPoss.Count(); j++)
            {
                if (!noCheck.Contains(j))
                {
                    for (int k = 0; k < indexesPoss.ElementAt(j).Count(); k++)
                    {
                        int val = indexesPoss.ElementAt(j).ElementAt(k);
                        if (unique.Contains(val) && Solved.zeroes.Contains(z.ElementAt(j)))
                        {
                            Solved.Reconstruct(z.ElementAt(j), val);
                            //Solved.board[z.ElementAt(j)] = val;
                            Solved.zeroes.Remove(z.ElementAt(j));
                        }
                    }
                }
            }
        }
    }
}
