using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class SudokuRules
    {
        public bool CanNumberBePlaced(Grid grid, int row, int column, int number)
        {
            return DoesRowAllowPlacement(grid, row, number)
                && DoesColumnAllowPlacement(grid, column, number)
                && DoesBoxAllowPlacement(grid, row, column, number);
        }

        public bool DoesRowAllowPlacement(Grid grid, int row, int number)
        {
            throw new NotImplementedException();
        }

        public bool DoesColumnAllowPlacement(Grid grid, int column, int number)
        {
            throw new NotImplementedException();
        }

        public bool DoesBoxAllowPlacement(Grid grid, int row, int column, int number)
        {
            throw new NotImplementedException();
        }

        public List<Cell> GetCellsInRow(Grid grid, int row)
        {
            throw new NotImplementedException();
        }

        public List<Cell> GetCellsInColumn(Grid grid, int column)
        {
            throw new NotImplementedException();
        }

        public List<Cell> GetCellsInBox(Grid grid, int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
