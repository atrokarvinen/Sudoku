using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests.Utils;

public static class SudokuFromText
{
    private const string EMPTY_CELL = " ";

    public static Grid Convert(string text)
    {
        int rowCount = 9;
        int columnCount = 9;
        Cell[][] cells = new Cell[rowCount][];

        string[] textRows = text
            .Replace("|", "")
            .Split('\r', '\n')
            .Where(rowStr => !rowStr.Contains("-"))
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

