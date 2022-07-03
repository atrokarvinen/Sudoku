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

        public void SetCellNote(int row, int column, int number)
        {
            Cell cell = GetCell(row, column);
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

        public string[] Rows => Cells.Select(row =>
        {
            string[] numbers = row.Select(cell => cell.Number?.ToString() ?? "0").ToArray();
            return string.Join("", numbers);
        }).ToArray();

        public double CompletionRate => GetCompletionRate();
        public double TotalNoteCount => GetCellsAsList().SelectMany(cell => cell.Notes).Count();

        private double GetCompletionRate()
        {
            int numberCount = GetCellsAsList().Where(cell => cell.Number.HasValue).Count();
            return 1.0 * numberCount / GetCellsAsList().Count;
        }
    }
}
