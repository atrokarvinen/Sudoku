using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {
        //public void StartGame() { }
        //public void QuitGame() { }

        [HttpPost("savegame")]
        [EnableCors]
        public IActionResult SaveGame()
        {
            return Ok();
        }

        [HttpGet("loadgame")]
        [EnableCors]
        public IActionResult LoadGame()
        {
            return Ok();
        }

        //public void Undo() { }
        //public void Redo() { }

        //#region New game creation

        //public void CreateNewGame() { }
        //public void AddNumber(int row, int column, int number) { }
        //public void AddNoteNumber(int row, int column, int number) { }

        //#endregion

    }
}
