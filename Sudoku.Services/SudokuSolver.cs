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

        public bool IsSudokuSolved(Grid grid)
        {
            List<Cell> cells = grid.GetCellsAsList();
            return cells.All(cell => _sudokuRules.CanNumberBePlaced(grid, cell.GridPoint, cell.Number.Value));
        }

        public Grid Solve(Sudoku.Domain.Sudoku sudoku)
        {
            Grid grid = sudoku.Grid;
            grid = QuickSolveNotes(grid);

            int maxSteps = 9 * 9;
            int step = 0;
            while (step < maxSteps)
            {
                try
                {
                    Cell solvedCell = SolveNextStep(grid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Run into problem with solving: {0}", ex.Message);
                    break;
                }

                step++;
            }

            bool isSolved = IsSudokuSolved(grid);

            return grid;
        }

        public Cell SolveNextStep(Grid sudoku)
        {
            //if (sudoku.GetCellsAsList().Any(cell => cell.Notes.Count() == 0 && cell.Number is null))
            sudoku = QuickSolveNotes(sudoku);

            int solvedNumber;
            GridPoint solvedCellLocation;

            Cell singleCandidate = SearchForSingleCandidates(sudoku);
            if (singleCandidate is null)
                throw new Exception("No solution found.");

            solvedNumber = singleCandidate.Notes.First();
            solvedCellLocation = new GridPoint(singleCandidate.Row, singleCandidate.Column);

            Cell solvedCell = sudoku.GetCell(solvedCellLocation);
            solvedCell.Number = solvedNumber;

            List<Cell> relatedCells = _sudokuRules.GetRelatedCells(sudoku, solvedCellLocation);
            relatedCells.ForEach(cell => cell.RemoveNote(solvedNumber));
            solvedCell.ResetNotes();

            // Todo must return list of updating cells. Otherwise related notes wont update.
            return solvedCell;
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

        public Cell SearchForSingleCandidates(Grid sudoku)
        {
            List<Cell> cells = sudoku.GetCellsAsList();
            return cells.Find(cell => cell.Notes.Count() == 1);
        }

        public void EliminatingNumbers() { }

        public void SearchForMissingNumbers() { }


        public void HasUniqueAnswer() { }
    }
}
