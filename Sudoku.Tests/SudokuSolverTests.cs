using FluentAssertions;
using Sudoku.Domain;
using Sudoku.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sudoku.Tests
{
    public class SudokuSolverTests
    {
        private const string SimpleSudokuFile = "Sudoku_simple.txt";

        [Fact]
        public void TestSolve()
        {
            ISudokuRules rules = new StandardSudokuRules();
            ISudokuSolver solver = new SudokuSolver(rules);

            Domain.Sudoku sudoku = SudokuTestUtils.LoadSudokuFromFile(SimpleSudokuFile);
            Grid grid = sudoku.Grid;

            //Cell solvedCell = solver.SolveNextStep(grid);
            solver.Solve(sudoku);
        }
    }
}
