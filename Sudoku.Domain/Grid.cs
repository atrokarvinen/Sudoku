using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Domain
{
    public class Grid
    {
        public Cell[][] Cells { get; set; }

        public Grid()
        {

        }

        public Grid(Cell[][] cells)
        {
            Cells = cells;
        }

        public Cell GetCell(int row, int column)
        {
            return Cells[row][column];
        }

        public Cell GetCell(GridPoint gridPoint)
        {
            var (row, column) = gridPoint;
            return GetCell(row, column);
        }

        public List<Cell> GetCellsAsList()
        {
            List<Cell> cellList = new List<Cell>();
            for (int row = 0; row < Cells.Length; row++)
            {
                for (int column = 0; column < Cells[row].Length; column++)
                {
                    cellList.Add(GetCell(new GridPoint(row, column)));
                }
            }

            return cellList;
        }

        public void SetCellNumber(GridPoint gridPoint, int? number)
        {
            Cell cell = GetCell(gridPoint);
            cell.Number = number;
        }

        public void SetCellNote(GridPoint gridPoint, int number)
        {
            Cell cell = GetCell(gridPoint);
            cell.AddNote(number);
        }

        public void ResetCellNotes(GridPoint gridPoint)
        {
            Cell cell = GetCell(gridPoint);
            cell.ResetNotes();
        }

        public void RemoveCellNote(GridPoint gridPoint, int number)
        {
            GetCell(gridPoint).RemoveNote(number);
        }

        public string ShowAsText()
        {
            List<string> numbers = Cells.SelectMany(row => row.Select(cell => CellToSudokuSymbol(cell))).ToList();
            return string.Join(",", numbers);
        }

        private string CellToSudokuSymbol(Cell cell) => NullableIntToSudokuSymbol(cell.Number);
        private string NullableIntToSudokuSymbol(int? number) => number?.ToString() ?? "-";
    }
}
