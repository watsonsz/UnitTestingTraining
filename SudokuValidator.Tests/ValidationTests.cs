using Shouldly;
using SudokuSolver.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuValidator.Tests
{
    public class ValidationTests
    {
        public static IEnumerable<object[]> WinningArrayData =>
        new List<object[]>
        {
            new object[]
            {
                new int[][]
                {
                    new int[] { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                    new int[] { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                    new int[] { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                    new int[] { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                    new int[] { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                    new int[] { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                    new int[] { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                    new int[] { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                    new int[] { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
                },
                true
            }
        };
        public static IEnumerable<object[]> LosingArrayData =>
            new List<object[]>
            {
                new object[]
                {
                    new int[][]
                    {
                        new int[] {5,3,4,6,7,8,9,1,2},
                        new int[] {6,7,2,1,9,0,3,4,8},
                        new int[] {1,0,0,3,4,2,5,6,0},
                        new int[] {8,5,9,7,6,1,0,2,0},
                        new int[] {4,2,6,8,5,3,7,9,1},
                        new int[] {7,1,3,9,2,4,8,5,6},
                        new int[] {9,0,1,5,3,7,2,1,4},
                        new int[] {2,8,7,4,1,9,6,3,5},
                        new int[] {3,0,0,4,8,1,1,7,9}
                    },
                    false
                }
            };

        [Theory]
        [MemberData(nameof(WinningArrayData))]
        [MemberData(nameof(LosingArrayData))]
        public void Check_Rows_Validation(int[][] board, bool result)
        {
            var validator = new SudokuSolver.Program.SudokuValidator();
            validator.CheckRows(board).ShouldBe(result);

        }

        [Theory]
        [MemberData(nameof(WinningArrayData))]
        public void GetColumns_GivenWinningArray_ReturnsColumnsAsRowsAndValidates(int[][] board, bool result)
        {
            var validator = new SudokuSolver.Program.SudokuValidator();
            int[] firstColumn = new int[] { 5, 6, 1, 8, 4, 7, 9, 2, 3 };
            var newArray = validator.GetColumns(board);
            newArray[0].ShouldBeEquivalentTo(firstColumn);
            validator.CheckRows(newArray).ShouldBe(result);
        }

        [Theory]
        [MemberData(nameof(WinningArrayData))]
        public void GetSquares_GivenWinningArray_ReturnsListOfSquaresAndValidates(int[][] board,bool result)
        {
            var validator = new SudokuSolver.Program.SudokuValidator();
            Square validationSquare = new Square
            {
                squareArray = new int[][]
                {
                    new int[] { 5, 3, 4 },
                    new int[] { 6, 7, 2 },
                    new int[] { 1, 9, 8 }
                }
            };
            var squares = validator.GetSquares(board);
            squares[0].squareArray.ShouldBeEquivalentTo(validationSquare.squareArray);
            validator.CheckSquares(board).ShouldBe(true);
            
        }

        [Theory]
        [MemberData(nameof(WinningArrayData))]
        [MemberData(nameof(LosingArrayData))]
        public void ValidateGame_GivenData_ReturnsExpectedResult(int[][] board,bool result)
        {
            var validator = new SudokuSolver.Program.SudokuValidator();
            validator.ValidateGame(board).ShouldBe(result);

        }
    }
}
