using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests.Utils;

public static class SudokuGenerator
{
    private const string EMPTY_CELL = " ";

    public static Grid EmptySudoku(int boxSize = 3)
    {
        int gridSize = boxSize * boxSize;
        Cell[][] cells = new Cell[gridSize][];
        for (int row = 0; row < gridSize; row++)
        {
            Cell[] rowCells = Enumerable.Range(0, gridSize)
                .Select(column => new Cell(row, column, number: null))
                .ToArray();
            cells[row] = rowCells;
        }
        return new Grid() { Cells = cells};
    }

    public static Grid FromText(string text)
    {
        int rowCount = 9;
        int columnCount = 9;
        Cell[][] cells = new Cell[rowCount][];

        string[] textRows = text
            .Replace("|", "")
            .Split('\r', '\n')
            .Where(rowStr => !rowStr.Contains("-") && rowStr.Length > 0)
            .Select(stringRow => stringRow.Substring(stringRow.Length - columnCount, columnCount))
            .ToArray();

        string sudokuText = string.Join("", textRows);

        for (int row = 0; row < rowCount; row++)
        {
            cells[row] = new Cell[rowCount];
            for (int column = 0; column < rowCount; column++)
            {
                string cellNumberStr = sudokuText[row * rowCount + column].ToString();
                int? number = cellNumberStr == EMPTY_CELL ? null : int.Parse(cellNumberStr);
                cells[row][column] = new Cell(row, column, number);
            }
        }

        return new Grid(cells);
    }
}

