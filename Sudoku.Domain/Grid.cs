using System;
using System.Linq;

namespace Sudoku.Domain
{
    public class Grid
    {
        private const string EmptyCellIdentified = "-";

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
                    int? number = cellNumberStr == EmptyCellIdentified ? null : int.Parse(cellNumberStr);
                    cells[row][column] = new Cell(row, column, number);
                }
            }

            return cells;
        }

        public string ShowAsText()
        {
            return string.Join(",", 
                Cells.Select(row =>
                    row.Select(cell => 
                        cell.Number?.ToString() ?? EmptyCellIdentified)
                    )
                );
        }
    }
}
