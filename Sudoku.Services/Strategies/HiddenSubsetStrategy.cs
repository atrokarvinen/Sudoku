using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

/// <summary>
/// When n candidates are possible in a certain set of n cells all in the same
/// block, row, or column, and those n candidates are not possible elsewhere 
/// in that same block, row, or column, then no other candidates are possible in those cells.
/// </summary>
public class HiddenSubsetStrategy : EliminationStrategyBase
{
    private readonly ISudokuRules _sudokuRules;

    public HiddenSubsetStrategy(ISudokuRules sudokuRules)
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

    private IEnumerable<Elimination> Eliminate(Grid sudoku, Func<Cell, int> groupBy)
    {
        List<Elimination> eliminations = new List<Elimination>();
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Cell> emptyCells = cells.Where(cell => cell.Number is null);

        var cellGroups = emptyCells.GroupBy(groupBy);
        foreach (var cellGroup in cellGroups)
        {
            var partialEliminations = IterateSubsets(cellGroup);
            eliminations.AddRange(partialEliminations);
        }

        return eliminations;
    }

    private IEnumerable<Elimination> IterateSubsets(IEnumerable<Cell> cellGroup)
    {
        List<Elimination> eliminations = new List<Elimination>();

        int minSetSize = 2;
        int maxSetSize = 2;
        IEnumerable<int> setSizes = Enumerable.Range(2, maxSetSize - minSetSize + 1);
        foreach (int setSize in setSizes)
        {
            Dictionary<GridPoint, IEnumerable<int>> notesByPoint =
                cellGroup.ToDictionary(c => c.GridPoint, c => c.Notes);
            IEnumerable<GridPoint> points = cellGroup
                .Where(c => c.Notes.Count() > 1)
                .Select(c => c.GridPoint);
            var sets = new Math.Combinatorics().GetAllSubsets(points, setSize);

            // [1, 2, 3] - [1, 2]
            // [1 ,2]    - [1, 2]
            // [1 ,3]
            // [2 ,3]
            var setsWithSameNotes = sets.Select(set =>
            {
                IEnumerable<int> notes1 = notesByPoint[set.First()];
                IEnumerable<int> notes2 = notesByPoint[set.Last()];

                var noteSets1 = new Math.Combinatorics().GetAllSubsets(notes1, 2);
                var noteSets2 = new Math.Combinatorics().GetAllSubsets(notes2, 2);
                var noteUnion = noteSets1.Union(noteSets2);

                List<HashSet<int>> hiddenPairs = new List<HashSet<int>>();
                foreach (var notePair1 in noteSets1)
                {
                    foreach (var notePair2 in noteSets2)
                    {
                        if (notePair1.OrderBy(n => n).SequenceEqual(notePair2.OrderBy(n => n)))
                        {
                            hiddenPairs.Add(notePair1);
                        }
                    }
                }

                return new Tuple<HashSet<GridPoint>, List<HashSet<int>>> (set, hiddenPairs);
            })
                .Where(x => x.Item2.Count > 0);

            foreach (var set in setsWithSameNotes)
            {
                var (cellPoints, hiddenPairs) = set;
                IEnumerable<Cell> eliminatedCells = cellGroup.Where(c => cellPoints.Contains(c.GridPoint));
                IEnumerable<int> notesToPreserve = hiddenPairs.SelectMany(notes => notes);
                IEnumerable<int> notesToRemove = cellPoints
                    .SelectMany(point => notesByPoint[point])
                    .Distinct()
                    .Except(notesToPreserve);
                IEnumerable<Elimination> partialEliminations = eliminatedCells.SelectMany(c =>
                {
                    var eliminatedCellNotes = notesToRemove.Where(note => c.Notes.Contains(note));
                    return eliminatedCellNotes.Select(note => new Elimination(c.Row, c.Column, note));
                });
                eliminations.AddRange(partialEliminations);
            }
        }
        return eliminations;
    }
}
