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

        public Cell GetCell(GridPoint gridPoint)
        {
            var (row, column) = gridPoint;
            return Cells[row][column];
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

        public void ResetCellNotes(GridPoint gridPoint)
        {
            Cell cell = GetCell(gridPoint);
            cell.ResetNotes();
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
