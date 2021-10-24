using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public interface ISudokuProvider
    {
        public void SaveSudoku(Domain.Sudoku sudoku);
        
        public Domain.Sudoku LoadSudoku(string gameName);
        public Domain.Sudoku LoadLatestSudoku();
    }
}
