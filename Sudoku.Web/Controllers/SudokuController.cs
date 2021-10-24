using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sudoku.Domain;
using Sudoku.Services;
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
        ISudokuProvider _sudokuProvider;

        public SudokuController(ISudokuProvider sudokuProvider)
        {
            _sudokuProvider = sudokuProvider;
        }

        //public void StartGame() { }
        //public void QuitGame() { }

        //[HttpPost("savegame")]
        //[EnableCors]
        //public IActionResult SaveGame()
        //{
        //    return Ok();
        //}

        [HttpPost("savegame")]
        [EnableCors]
        public IActionResult SaveGame([FromBody] Grid sudoku)
        {
            _sudokuProvider.SaveSudoku(new Domain.Sudoku() { Grid = sudoku });
            return Ok();
        }

        [HttpGet("loadgame/{gameName}")]
        [EnableCors]
        public ActionResult<Domain.Sudoku> LoadGame(string gameName)
        {
            return _sudokuProvider.LoadSudoku(gameName);
        }

        [HttpGet("loadgame")]
        [EnableCors]
        public ActionResult<Grid> LoadGame()
        {
            return _sudokuProvider.LoadLatestSudoku().Grid;
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
