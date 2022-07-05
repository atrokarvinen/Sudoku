using Sudoku.Domain;
using Sudoku.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services;

public record SolutionStep(
    ISudokuStrategy usedStrategy, 
    IEnumerable<SudokuSolutionBase> solutionsFound);

public class StrategySolver : ISudokuSolver
{
    private readonly ISudokuRules _sudokuRules;
    private readonly List<ISudokuStrategy> _strategies;

    public StrategySolver(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
        _strategies = new List<ISudokuStrategy>()
        {
            new ScanStrategy(_sudokuRules),
            new SingleCandidateStrategy(_sudokuRules),
            new LockedCandidatesStrategy(_sudokuRules),
            new NakedSubsetStrategy(_sudokuRules),
            //new HiddenSubsetStrategy(sudokuRules),
        };
    }

    public bool IsSudokuSolved(Grid grid)
    {
        List<Cell> cells = grid.GetCellsAsList();
        return cells.All(cell => cell.Number.HasValue && _sudokuRules.CanNumberBePlaced(grid, cell.GridPoint, cell.Number.Value));
    }

    public bool IsSudokuFilled(Grid grid)

    {
        return grid.GetCellsAsList().Where(c => c.Number.HasValue).Count() == 81;
    }

    public Grid QuickSolveNotes(Grid sudoku)
    {
        throw new NotImplementedException();
    }

    public Grid Solve(Domain.Sudoku sudoku)
    {
        int maxIterations = 999;
        int iteration = 0;
        Grid grid = sudoku.Grid;
        FillAllNotes(grid);
        while (!IsSudokuFilled(grid) && iteration < maxIterations)
        {
            try
            {
                SolutionStep solutionStep = GetNextSolutionStep(grid);
                var (strategy, solutions) = solutionStep;
                foreach (var solution in solutions)
                {
                    strategy.ApplySolution(grid, solution);
                }
                iteration++;
            }
            catch (Exception ex)
            {
                break;
                throw;
            }
        }

        return grid;
    }

    private void FillAllNotes(Grid grid)
    {
        var cells = grid.GetCellsAsList();
        var emptyCells = cells.Where(c => c.Number is null).ToList();
        foreach (var emptyCell in emptyCells)
        {
            Enumerable.Range(1, 9).ToList().ForEach(number => emptyCell.AddNote(number));
        }
    }

    public Cell SolveNextStep(Grid sudoku)
    {
        SudokuSolutionBase solution = GetNextSolution(sudoku);
        if (solution is null)
            throw new Exception("No more solutions could be found.");
        return null;
    }

    public SudokuSolutionBase GetNextSolution(Grid sudoku)
    {
        foreach (var strategy in _strategies)
        {
            IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(sudoku);
            if (solutions.Any())
            {
                return solutions.First();
            }
        }
        return null;
    }

    public IEnumerable<SudokuSolutionBase> GetNextSolutions(Grid sudoku)
    {
        foreach (var strategy in _strategies)
        {
            IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(sudoku);
            if (solutions.Any())
            {
                return solutions;
            }
        }
        throw new Exception("No more solutions could be found.");
    }
    
    public SolutionStep GetNextSolutionStep(Grid sudoku)
    {
        foreach (var strategy in _strategies)
        {
            IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(sudoku);
            if (solutions.Any())
            {
                return new SolutionStep(strategy, solutions);
            }
        }
        throw new Exception("No more solutions could be found.");
    }
}
