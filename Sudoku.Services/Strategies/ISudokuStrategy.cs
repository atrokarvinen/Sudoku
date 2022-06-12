using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services.Strategies;

public abstract class SudokuSolutionBase
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int Number { get; set; }
    public GridPoint GridPoint => new GridPoint(Row, Column);

    protected SudokuSolutionBase(int row, int column, int number)
    {
        Row = row;
        Column = column;
        Number = number;
    }

    public abstract void ApplySolution(Grid sudoku);
}

public class Elimination: SudokuSolutionBase
{
    public Elimination(int row, int column, int number) : base(row, column, number)
    {

    }

    public override void ApplySolution(Grid sudoku)
    {
        sudoku.RemoveCellNote(GridPoint, Number);
    }
}

public class Addition : SudokuSolutionBase
{
    public Addition(int row, int column, int number) : base(row, column, number)
    {

    }

    public override void ApplySolution(Grid sudoku)
    {
        sudoku.SetCellNumber(GridPoint, Number);
        sudoku.ResetCellNotes(GridPoint);
    }
}

public interface ISudokuStrategy
{
    IEnumerable<SudokuSolutionBase> Solve(Grid sudoku);
}
