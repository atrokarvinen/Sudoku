using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

public class SingleCandidateStrategy : AdditionStrategyBase
{
    private readonly ISudokuRules _sudokuRules;

    public SingleCandidateStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public override IEnumerable<SudokuSolutionBase> Solve(Grid sudoku)
    {
        IEnumerable<Addition> singleCandidates = SolveSingleCells(sudoku);
        IEnumerable<Addition> rowSolutions = SolveRows(sudoku);
        IEnumerable<Addition> columnSolutions = SolveColumns(sudoku);
        IEnumerable<Addition> boxSolutions = SolveBoxes(sudoku);

        List<Addition> allSolutions = new List<Addition>();
        allSolutions.AddRange(singleCandidates);
        allSolutions.AddRange(rowSolutions);
        allSolutions.AddRange(columnSolutions);
        allSolutions.AddRange(boxSolutions);

        IEnumerable<Addition> uniqueSolutions = allSolutions.Distinct();

        return uniqueSolutions;
    }

    public IEnumerable<Addition> SolveSingleCells(Grid sudoku)
    {
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Cell> cellsWithSingleCandidate = cells.Where(cell => cell.Notes.Count() == 1);
        IEnumerable<Addition> additions = cellsWithSingleCandidate.Select(cell =>
        {
            int number = cell.Notes.Single();
            (int row, int column) = cell.GridPoint;
            return new Addition(row, column, number);
        });
        return additions;
    }

    public IEnumerable<Addition> SolveRows(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Row;
        return SolveCandidates(sudoku, groupBy);
    }

    public IEnumerable<Addition> SolveColumns(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Column;
        return SolveCandidates(sudoku, groupBy);
    }

    public IEnumerable<Addition> SolveBoxes(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => _sudokuRules.GetCellBoxIndex(cell);
        return SolveCandidates(sudoku, groupBy);
    }

    private IEnumerable<Addition> SolveCandidates(Grid sudoku, Func<Cell, int> groupBy)
    {
        List<Addition> additions = new List<Addition>();
        List<Cell> cells = sudoku.GetCellsAsList();
        var cellsGrouped = cells.GroupBy(groupBy);
        foreach (var cellGroup in cellsGrouped)
        {
            foreach (var cell in cellGroup)
            {
                foreach (var note in cell.Notes)
                {
                    var otherNotes = cellGroup
                        .Where(c => c.GridPoint != cell.GridPoint)
                        .SelectMany(cell => cell.Notes);
                    if (!otherNotes.Contains(note))
                    {
                        additions.Add(new Addition(cell.Row, cell.Column, note));
                        break;
                    }
                }
            }
        }
        return additions;
    }
}
