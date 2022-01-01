using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class StandardSudokuRules: ISudokuRules
    {
        private CellExtractor CellExtractor = new CellExtractor();

        public bool IsCellEmpty(Grid grid, GridPoint point)
        {
            return grid.Cells[point.Row][point.Column].Number is null;
        }

        public bool CanNumberBePlaced(Grid grid, GridPoint gridPoint, int number)
        {
            (int row, int column) = gridPoint;
            return DoesRowAllowPlacement(grid, row, number)
                && DoesColumnAllowPlacement(grid, column, number)
                && DoesBoxAllowPlacement(grid, gridPoint, number);
        }

        public bool DoesRowAllowPlacement(Grid grid, int row, int number)
        {
            List<Cell> rowCells = GetCellsInRow(grid, row);
            return rowCells.TrueForAll(cell => cell.Number != number);
        }

        public bool DoesColumnAllowPlacement(Grid grid, int column, int number)
        {
            List<Cell> columnCells = GetCellsInColumn(grid, column);
            return columnCells.TrueForAll(cell => cell.Number != number);
        }

        public bool DoesBoxAllowPlacement(Grid grid, GridPoint gridPoint, int number)
        {
            List<Cell> boxCells = GetCellsInBox(grid, gridPoint);
            return boxCells.TrueForAll(x => x.Number != number);
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
            int boxSize = 3;
            int boxMin = location - (location % boxSize);
            return Enumerable.Range(boxMin, boxSize).ToArray();
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
            relatedCells = relatedCells.GroupBy(cell => cell.GridPoint)
                .First()
                .ToList();

            return relatedCells;
        }
    }
}
