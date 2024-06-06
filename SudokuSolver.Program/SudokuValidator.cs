using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Program
{
    public class SudokuValidator
    {
        public const int ROW_LENGTH_INDEX = 8;
        public int[][] board = new int[9][];

        public bool ValidateGame(int[][] board)
        {
            var rowsCheck = CheckRows(board);
            var columns = GetColumns(board);
            var columnCheck = CheckRows(board);
            var squares = CheckSquares(board);
            if(!rowsCheck || !columnCheck || !squares)
            {
                return false;
            }

            return true;
        }

        public bool CheckRows(int[][] board)
        {
            for(int i = 0; i < board.Length; i++)
            {
                HashSet<int> seenNumbers = new HashSet<int>();
                for (int j = 0; j < board[i].Length; j++)
                {
                    
                    if (!seenNumbers.Add(board[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int[][] GetColumns(int[][] board)
        {
            int[][] columnArrays = new int[board.Length][];
            for(int k = 0; k < board.Length; k++)
            {
                int[] column = new int[board.Length];
                for (int i = 0; i < board.Length; i++)
                {
                    
                    column[i]= board[i][k];
                }
                columnArrays[k] = column;
               
            }
            return columnArrays;
            
        }

        public bool CheckSquares(int[][] board)
        {
            var squares = GetSquares(board);
            foreach( var square in squares )
            {
                var row = CheckRows(square.squareArray);
                var columns = GetColumns(square.squareArray);
                var columnCheck = CheckRows(columns);
                if(!row || !columnCheck)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Square> GetSquares(int[][] board)
        {
            int i = 2;
            int square_corner_index = 0;
            List<Square> squares = new List<Square>();
            while(square_corner_index <= ROW_LENGTH_INDEX)
            {
                var square = new Square();
                //row indexes
                int squareindex = 0;
                for(int j = i-2; j <= i; j++)
                {
                    var row = new int[3];
                    int rowindex = 0;
                    for(int k = square_corner_index; k < square_corner_index+3; ++k)
                    {
                        row[rowindex] = board[j][k];
                        rowindex++;
                    }
                    square.squareArray[squareindex]=row;
                    squareindex++;
                }
                squares.Add(square);
                if(i == ROW_LENGTH_INDEX)
                {
                    i = 2;
                    square_corner_index += 3;
                    continue;
                }
                i += 3;
            }
            return squares;
            
        }

        
    }

    public class Square
    {
        public int[][] squareArray = new int[3][];
    }

    
}
