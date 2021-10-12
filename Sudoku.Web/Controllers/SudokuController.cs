using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Web.Controllers
{
    public class SudokuController
    {
        public void StartGame() { }
        public void QuitGame() { }

        public void SaveGame() { }
        public void LoadGame() { }

        public void Undo() { }
        public void Redo() { }

        #region New game creation

        public void CreateNewGame() { }
        public void AddNumber(int row, int column, int number) { }
        public void AddNoteNumber(int row, int column, int number) { }

        #endregion

    }
}
