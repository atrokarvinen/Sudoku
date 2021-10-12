using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public interface ISolver
    {
        public Grid Solve(Sudoku.Domain.Sudoku sudoku);

    }
}
