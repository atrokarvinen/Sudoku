using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class Solver : ISolver
    {
        public Grid Solve(Sudoku.Domain.Sudoku sudoku)
        {
            Grid grid = sudoku.Grid;
            throw new NotImplementedException();
        }

        public void ScanRows() { }

        public void HasUniqueAnswer() { }
    }
}
