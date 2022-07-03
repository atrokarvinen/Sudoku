using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services;

public class StandardSudokuRules : ISudokuRules
{
    public int BoxSize { get; set; } = 3;

    public bool IsCellEmpty(Grid grid, GridPoint point)
    {
        return grid.Cells[point.Row][point.Column].Number is null;
    }

    public bool CanNumberBePlaced(Grid grid, GridPoint gridPoint, int number)
    {
        (int row, int column) = gridPoint;
        return DoesRowAllowPlacement(grid, gridPoint, number)
            && DoesColumnAllowPlacement(grid, gridPoint, number)
            && DoesBoxAllowPlacement(grid, gridPoint, number);
    }

    public bool DoesRowAllowPlacement(Grid grid, GridPoint gridPoint, int number)
    {
        (int row, _) = gridPoint;
        List<Cell> rowCells = GetCellsInRow(grid, row);
        return IsCellListValid(rowCells, gridPoint, number);
    }

    public bool DoesColumnAllowPlacement(Grid grid, GridPoint gridPoint, int number)
    {
        (_, int column) = gridPoint;
        List<Cell> columnCells = GetCellsInColumn(grid, column);
        return IsCellListValid(columnCells, gridPoint, number);
    }

    public bool DoesBoxAllowPlacement(Grid grid, GridPoint gridPoint, int number)
    {
        List<Cell> boxCells = GetCellsInBox(grid, gridPoint);
        return IsCellListValid(boxCells, gridPoint, number);
    }

    public List<Cell> GetCellsInRow(Grid grid, int row)
    {
        return grid.Cells[row].ToList();
    }

    public List<Cell> GetCellsInColumn(Grid grid, int column)
    {
        return grid.Cells.Select(row => row[column]).ToList();
    }

    public List<Cell> GetCellsInBox(Grid grid, GridPoint gridPoint)
    {
        (int row, int column) = gridPoint;
        int[] rowRange = GetBoxRange(row);
        int[] columnRange = GetBoxRange(column);
        List<Cell> boxCells = new List<Cell>();
        foreach (var boxRow in rowRange)
        {
            foreach (var boxColumn in columnRange)
            {
                boxCells.Add(grid.Cells[boxRow][boxColumn]);
            }
        }

        return boxCells;
    }

    private int[] GetBoxRange(int location)
    {
        int boxSize = BoxSize;
        int boxMin = location - (location % boxSize);
        return Enumerable.Range(boxMin, boxSize).ToArray();
    }

    private bool IsCellListValid(List<Cell> cells, GridPoint gridPoint, int number)
    {
        return cells.TrueForAll(cell => IsPlacementValid(cell, gridPoint, number));
    }

    private bool IsPlacementValid(Cell cell, GridPoint gridPoint, int number)
    {
        bool isNumberSame = cell.Number == number;
        bool isSelf = cell.GridPoint == gridPoint;
        bool isValid = (isSelf && isNumberSame) || !isNumberSame;
        //if (!isValid)
        //{
        //    Cell cellToPlace = new Cell(gridPoint.Row, gridPoint.Column, number);
        //    throw new Exception($"Cannot place cell ({cellToPlace}) close to cell {cell}");
        //}
        return isValid;
    }

    public List<Cell> GetRelatedCells(Grid sudoku, GridPoint gridPoint)
    {
        var (row, columm) = gridPoint;

        List<Cell> rowCells = GetCellsInRow(sudoku, row);
        List<Cell> columnCells = GetCellsInColumn(sudoku, columm);
        List<Cell> boxCells = GetCellsInBox(sudoku, gridPoint);

        List<Cell> relatedCells = new List<Cell>();
        relatedCells.AddRange(rowCells);
        relatedCells.AddRange(columnCells);
        relatedCells.AddRange(boxCells);

        // Todo find a way to remove duplicates from box, row and column overlap.
        //relatedCells = relatedCells.GroupBy(cell => cell.GridPoint)
        //    .First()
        //    .ToList();

        return relatedCells;
    }

    public int GetCellBoxIndex(Cell cell)
    {
        //0 1 2
        //3 4 5
        //6 7 8
        int rowIndex = cell.Row / BoxSize;
        int columnIndex = cell.Column / BoxSize;
        return rowIndex + columnIndex * BoxSize;
    }
}

