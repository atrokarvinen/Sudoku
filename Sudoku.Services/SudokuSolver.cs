using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class SudokuSolver : ISudokuSolver
    {
        private readonly ISudokuRules _sudokuRules;

        public SudokuSolver(ISudokuRules sudokuRules)
        {
            _sudokuRules = sudokuRules;
        }

        public Grid Solve(Sudoku.Domain.Sudoku sudoku)
        {
            Grid grid = sudoku.Grid;
            throw new NotImplementedException();
        }

        public Cell SolveNextStep(Grid sudoku)
        {
            throw new NotImplementedException();
        }

        public Grid QuickSolveNotes(Grid sudoku)
        {
            int countNumbers = 9;
            Cell[][] cells = sudoku.Cells;
            for (int row = 0; row < cells.Length; row++)
            {
                for (int column = 0; column < cells[row].Length; column++)
                {
                    GridPoint gridPoint = new GridPoint(row, column);
                    sudoku.ResetCellNotes(gridPoint);
                    bool isEmpty = _sudokuRules.IsCellEmpty(sudoku, gridPoint);
                    if (!isEmpty)
                        continue;

                    for (int number = 1; number <= countNumbers; number++)
                    {
                        bool isLegal = _sudokuRules.CanNumberBePlaced(sudoku, gridPoint, number);
                        if (!isLegal)
                            continue;

                        sudoku.SetCellNote(gridPoint, number);
                    }
                }
            }

            return sudoku;
        }

        public void ScanInOneDirection() { }

        public void ScanInTwoDirections() { }

        public void SearchForSingleCandidates() { }

        public void EliminatingNumbers() { }

        public void SearchForMissingNumbers() { }


        public void HasUniqueAnswer() { }
    }
}
