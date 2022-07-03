using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

/// <summary>
/// When n candidates are possible in a certain set of n cells all in the same 
/// block, row, or column, and no other candidates are possible in those cells, 
/// then those n candidates are not possible elsewhere in that same block, row, or column.
/// </summary>
public class NakedSubsetStrategy : EliminationStrategyBase
{
    private readonly ISudokuRules _sudokuRules;

    public NakedSubsetStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public override IEnumerable<Elimination> Solve(Grid sudoku)
    {
        IEnumerable<Elimination> rowEliminations = EliminateRows(sudoku);
        IEnumerable<Elimination> columnEliminations = EliminateColumns(sudoku);
        IEnumerable<Elimination> boxEliminations = EliminateBoxes(sudoku);

        List<Elimination> allEliminations = new List<Elimination>();
        allEliminations.AddRange(rowEliminations);
        allEliminations.AddRange(columnEliminations);
        allEliminations.AddRange(boxEliminations);

        IEnumerable<Elimination> uniqueEliminations = allEliminations.Distinct();

        return uniqueEliminations;
    }

    private IEnumerable<Elimination> EliminateRows(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Row;
        return Eliminate(sudoku, groupBy);
    }

    private IEnumerable<Elimination> EliminateColumns(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Column;
        return Eliminate(sudoku, groupBy);
    }

    private IEnumerable<Elimination> EliminateBoxes(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => _sudokuRules.GetCellBoxIndex(cell);
        return Eliminate(sudoku, groupBy);
    }

    private IEnumerable<Elimination> IterateSubsets(IEnumerable<Cell> cellGroup)
    {
        List<Elimination> eliminations = new List<Elimination>();

        int minSetSize = 2;
        int maxSetSize = 4;
        IEnumerable<int> setSizes = Enumerable.Range(2, maxSetSize - minSetSize + 1);
        foreach (int setSize in setSizes)
        {
            Dictionary<GridPoint, IEnumerable<int>> notesByPoint =
                cellGroup.ToDictionary(c => c.GridPoint, c => c.Notes);
            IEnumerable<GridPoint> points = cellGroup
                .Where(c => c.Notes.Count() == setSize)
                .Select(c => c.GridPoint);
            var pairs = new Math.Combinatorics().GetAllSubsets(points, setSize);
            var pairsWithSameNotes = pairs.Where(pair =>
            {
                var notes1 = notesByPoint[pair.First()];
                var notes2 = notesByPoint[pair.Last()];

                return notes1.Union(notes2).Count() == notes1.Count()
                    && notes1.Count() == notes2.Count()
                    && notes1.Count() > 0;
            });

            foreach (var pair in pairsWithSameNotes)
            {
                IEnumerable<Cell> nonPairedCells = cellGroup
                    .Where(c => !pair.Contains(c.GridPoint));

                IEnumerable<int> notesToRemove = notesByPoint[pair.First()];
                IEnumerable<Elimination> partialEliminations = nonPairedCells.SelectMany(c =>
                {
                    var eliminatedCellNotes = notesToRemove.Where(note => c.Notes.Contains(note));
                    return eliminatedCellNotes.Select(note => new Elimination(c.Row, c.Column, note));
                });
                eliminations.AddRange(partialEliminations);
            }
        }
        return eliminations;
    }


    private IEnumerable<Elimination> Eliminate(Grid sudoku, Func<Cell, int> groupBy)
    {
        List<Elimination> eliminations = new List<Elimination>();
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Cell> emptyCells = cells.Where(cell => cell.Number is null);

        //int setSize = 2;
        var cellGroups = emptyCells.GroupBy(groupBy);
        foreach (var cellGroup in cellGroups)
        {
            //Dictionary<GridPoint, IEnumerable<int>> notesByPoint =
            //    cellGroup.ToDictionary(c => c.GridPoint, c => c.Notes);
            //IEnumerable<GridPoint> points = cellGroup.Select(c => c.GridPoint);
            //var pairs = new Math.Combinatorics().GetAllSubsets(points, setSize);
            //var pairsWithSameNotes = pairs.Where(pair =>
            //{
            //    var notes1 = notesByPoint[pair.First()];
            //    var notes2 = notesByPoint[pair.Last()];

            //    return notes1.Union(notes2).Count() == notes1.Count()
            //        && notes1.Count() == notes2.Count()
            //        && notes1.Count() > 0;
            //});

            //if (!pairsWithSameNotes.Any())
            //    continue;

            //HashSet<GridPoint> pair = pairsWithSameNotes.First();
            //IEnumerable<Cell> nonPairedCells = cellGroup
            //    .Where(c => !pair.Contains(c.GridPoint));

            //IEnumerable<int> notesToRemove = notesByPoint[pair.First()];
            //IEnumerable<Elimination> partialEliminations = nonPairedCells.SelectMany(c =>
            //{
            //    var eliminatedCellNotes = notesToRemove.Where(note => c.Notes.Contains(note));
            //    return eliminatedCellNotes.Select(note => new Elimination(c.Row, c.Column, note));
            //});
            //eliminations.AddRange(partialEliminations);
            var partialEliminations = IterateSubsets(cellGroup);
            eliminations.AddRange(partialEliminations);
        }

        return eliminations;
    }
}
