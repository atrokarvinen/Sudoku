using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

public class ScanStrategy : EliminationStrategyBase
{
    private readonly ISudokuRules _sudokuRules;

    public ScanStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public override IEnumerable<SudokuSolutionBase> Solve(Grid sudoku)
    {
        IEnumerable<Elimination> rowSolutions = SolveRows(sudoku);
        IEnumerable<Elimination> columnSolutions = SolveColumns(sudoku);
        IEnumerable<Elimination> boxSolutions = SolveBoxes(sudoku);

        List<Elimination> allSolutions = new List<Elimination>();
        allSolutions.AddRange(rowSolutions);
        allSolutions.AddRange(columnSolutions);
        allSolutions.AddRange(boxSolutions);

        IEnumerable<Elimination> uniqueSolutions = allSolutions.Distinct();

        return uniqueSolutions;
    }

    public IEnumerable<Elimination> SolveRows(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Row;
        return Scan(sudoku, groupBy);
    }

    public IEnumerable<Elimination> SolveColumns(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Column;
        return Scan(sudoku, groupBy);
    }

    public IEnumerable<Elimination> SolveBoxes(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => _sudokuRules.GetCellBoxIndex(cell);
        return Scan(sudoku, groupBy);
    }

    private IEnumerable<Elimination> Scan(Grid sudoku, Func<Cell, int> groupBy)
    {
        List<Elimination> eliminations = new List<Elimination>();
        List<Cell> cells = sudoku.GetCellsAsList();
        var cellGroups = cells.GroupBy(groupBy);
        foreach (var cellGroup in cellGroups)
        {
            IEnumerable<int> numbersInGroup = cellGroup
                .Where(c => c.Number is not null)
                .Select(c => c.Number.Value);
            IEnumerable<Cell> emptyCells = cellGroup.Where(c => c.Number is null);

            IEnumerable<Elimination> partialEliminations = emptyCells
                .SelectMany(c =>
                {
                    return c.Notes
                        .Where(note => numbersInGroup.Contains(note))
                        .Select(note => new Elimination(c.Row, c.Column, note));
                });

            eliminations.AddRange(partialEliminations);
        }
        return eliminations;
    }
}
