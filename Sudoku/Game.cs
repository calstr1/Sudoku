using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;

namespace Sudoku
{
    class Game
    {
        public static List<int> zeroes = new List<int>();
        public static List<int> options = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        public static void Main(string[] args)
        {
            string line = Console.ReadLine();
            int[] input = line.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            Board.Fill(input);
            Board.PrintBoard();


            while (zeroes.Count() != 0)
            {
                int initial = zeroes.Count();
                for (int i = 0; i < zeroes.Count(); i++)
                {
                    int boardIndex = zeroes.ElementAt(i);
                    List<int> currPoss = PossList(boardIndex);
                    if (currPoss.Count() == 1 && zeroes.Contains(boardIndex))
                    {
                        int num = currPoss.ElementAt(0);
                        Board.board[boardIndex] = num;
                        Board.Reconstruct(boardIndex, num);
                        zeroes.Remove(boardIndex);
                    }
                    else
                    {
                        //return from zero list matching row, find unique possibility, implrement, repeat for squ and col
                        int row = boardIndex / 9;
                        int col = boardIndex % 9;
                        int squ = (3 * (row / 3)) + (col / 3);
                        int[] rowIndx = new int[9];
                        int[] colIndx = new int[9];
                        int[] squIndx = new int[9];
                        for (int a = 0; a < 9; a++)
                        {
                            rowIndx[a] = (row * 9) + a;
                            colIndx[a] = col + (9 * a);
                        }
                        for (int c = 0; c < 3; c++)
                        {
                            for (int d = 0; d < 3; d++)
                            {
                                squIndx[(c * 3) + (d)] = (((squ - squ % 3) + c) * 9) + ((squ % 3) * 3) + d;
                            }
                        }
                        List<int> zRow = new List<int>();
                        List<int> zCol = new List<int>();
                        List<int> zSqu = new List<int>();
                        for (int iter = 0; iter < 9; iter++)
                        {
                            int r1 = rowIndx[iter];
                            int c = colIndx[iter];
                            int s = squIndx[iter];
                            if (zeroes.Contains(r1)) zRow.Add(r1);
                            if (zeroes.Contains(c)) zCol.Add(c);
                            if (zeroes.Contains(s)) zSqu.Add(s);
                        }
                        IndexLogic(zRow);
                        IndexLogic(zCol);
                        IndexLogic(zSqu);
                    }
                }
                if (initial == zeroes.Count()) { Console.WriteLine("true"); break; }
            }
            Board.PrintBoard();
        


        Console.Read();
        }

        public static List<int> PossList(int i)
        {
            int row = i / 9;
            int col = i % 9;
            int squ = (3 * (row / 3)) + (col / 3);
            List<int> rcs = new List<int>();
            rcs.AddRange(Board.rows.GetArr(row));
            rcs.AddRange(Board.columns.GetArr(col));
            rcs.AddRange(Board.squares.GetArr(squ));
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
                currPoss = PossList(boardIndex);
                if (currPoss.Count() == 1 && zeroes.Contains(boardIndex))
                {
                    int num = currPoss.ElementAt(0);
                    Board.board[boardIndex] = num;
                    Board.Reconstruct(boardIndex, num);
                    zeroes.Remove(boardIndex);
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
                        if (unique.Contains(val) && zeroes.Contains(z.ElementAt(j)))
                        {
                            Board.Reconstruct(z.ElementAt(j), val);
                            Board.board[z.ElementAt(j)] = val;
                            zeroes.Remove(z.ElementAt(j));
                        }
                    }
                }
            }
        }
    }
}
