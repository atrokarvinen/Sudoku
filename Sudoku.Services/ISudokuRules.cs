using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services;

public interface ISudokuRules
{
    public bool IsCellEmpty(Grid grid, GridPoint point);

    public bool CanNumberBePlaced(Grid grid, GridPoint point, int number);
    public bool DoesRowAllowPlacement(Grid grid, int row, int number);
    public bool DoesColumnAllowPlacement(Grid grid, int column, int number);
    public bool DoesBoxAllowPlacement(Grid grid, GridPoint point, int number);
    public List<Cell> GetCellsInRow(Grid grid, int row);
    public List<Cell> GetCellsInColumn(Grid grid, int column);
    public List<Cell> GetCellsInBox(Grid grid, GridPoint gridPoint);
    public List<Cell> GetRelatedCells(Grid sudoku, GridPoint gridPoint);
}
