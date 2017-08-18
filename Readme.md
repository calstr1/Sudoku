# C# OcrSudoku

Work in progress by Callum Hussey and Reuben Goetz-Wylie, latest versions can be found at
(https://github.com/calstr1/OcrSudoku).

The goal of this project is to result in an app (preferably mobile) that allows a user to
take a picture of a physical sudoku game, have it digitalised, and allow the user to play it
with the options to have hints, and be warned when making either a wrong or illegal move.

Versions
0.1:  Solves Sudoku using human logic and reasoning

Sample Sudoku boards:
7,9,0,0,0,0,3,0,0,0,0,0,0,0,6,9,0,0,8,0,0,0,3,0,0,7,6,0,0,0,0,0,5,0,0,2,0,0,5,4,1,8,7,0,0,4,0,0,7,0,0,0,0,0,6,1,0,0,9,0,0,0,8,0,0,2,3,0,0,0,0,0,0,0,9,0,0,0,0,5,4
0,3,0,9,0,0,0,2,0,8,0,0,0,0,2,0,0,7,0,0,1,4,0,0,6,0,0,0,9,0,0,4,0,5,0,2,0,0,0,6,0,3,0,0,0,7,0,6,0,1,0,0,8,0,0,0,9,0,0,4,1,0,0,2,0,0,8,0,0,0,0,3,0,7,0,0,0,9,0,5,0
5,3,8,0,1,6,0,7,9,0,0,0,3,8,0,5,4,1,2,4,1,5,0,0,0,0,0,0,6,0,9,0,0,0,0,0,0,0,0,0,3,5,0,9,0,0,9,0,0,0,4,0,0,2,6,0,0,2,0,0,9,3,0,1,2,9,0,4,0,0,5,0,0,5,4,6,9,0,0,0,8