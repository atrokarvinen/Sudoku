using Sudoku.Domain;

namespace Sudoku.Services
{
    public interface ISudokuSolver
    {
        public Grid Solve(Sudoku.Domain.Sudoku sudoku);
        public Cell SolveNextStep(Grid sudoku);
        public Grid QuickSolveNotes(Grid sudoku);
        public bool IsSudokuSolved(Grid grid);
    }
}
