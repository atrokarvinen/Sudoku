using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Domain
{
    public class Grid
    {
        private const string EmptyCellIdentifier = "-";

        public Cell[][] Cells { get; set; }

        public Grid()
        {

        }

        public Grid(string sudokuAsText)
        {
            Cells = GetCellsFromText(sudokuAsText);
        }

        public Cell[][] GetCellsFromText(string text)
        {
            string[] cellsText = text.Split(',');
            
            int rowCount = 9;
            Cell[][] cells = new Cell[rowCount][];

            for (int row = 0; row < rowCount; row++)
            {
                cells[row] = new Cell[rowCount];
                for (int column = 0; column < rowCount; column++)
                {
                    string cellNumberStr = cellsText[row * rowCount + column];
                    int? number = cellNumberStr == EmptyCellIdentifier ? null : int.Parse(cellNumberStr);
                    cells[row][column] = new Cell(row, column, number);
                }
            }

            return cells;
        }

        public string ShowAsText()
        {
            List<string> numbers = Cells.SelectMany(row => row.Select(cell => CellToSudokuSymbol(cell))).ToList();
            return string.Join(",", numbers);
        }

        private string CellToSudokuSymbol(Cell cell) => NullableIntToSudokuSymbol(cell.Number);
        private string NullableIntToSudokuSymbol(int? number) => number?.ToString() ?? EmptyCellIdentifier;
    }
}
