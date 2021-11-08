using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class SudokuSolver : ISudokuSolver
    {
        public Grid Solve(Sudoku.Domain.Sudoku sudoku)
        {
            Grid grid = sudoku.Grid;
            throw new NotImplementedException();
        }

        public Cell SolveNextStep(Grid sudoku)
        {
            throw new NotImplementedException();
        }

        public void ScanInOneDirection() { }

        public void ScanInTwoDirections() { }

        public void SearchForSingleCandidates() { }

        public void EliminatingNumbers() { }

        public void SearchForMissingNumbers() { }


        public void HasUniqueAnswer() { }
    }
}
