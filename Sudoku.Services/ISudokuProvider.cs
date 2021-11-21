namespace Sudoku.Services
{
    public interface ISudokuProvider
    {
        public void SaveSudoku(Domain.Sudoku sudoku, string path);

        public Domain.Sudoku LoadSudoku(string gameFile);
        public Domain.Sudoku LoadLatestSudoku(string folder);
    }
}
