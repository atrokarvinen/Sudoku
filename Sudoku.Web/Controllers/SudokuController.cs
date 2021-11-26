using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sudoku.Domain;
using Sudoku.Services;
using Sudoku.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sudoku.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {
        readonly ISudokuProvider _sudokuProvider;
        readonly ISudokuSolver _sudokuSolver;

        ApplicationConfiguration _appConfig = new ApplicationConfiguration();

        public SudokuController(ISudokuProvider sudokuProvider, ISudokuSolver sudokuSolver)
        {
            _sudokuProvider = sudokuProvider;
            _sudokuSolver = sudokuSolver;

            //HttpClient httpClient = new HttpClient();
            //var result = httpClient.GetAsync("https://www.sudokuweb.org/").Result;
            //string contentString = result.Content.ReadAsStringAsync().Result;
            //System.IO.File.Create("Test.txt").Close();
            //System.IO.File.WriteAllText("Test.txt", contentString);
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
            Domain.Sudoku sudokuToSave = new Domain.Sudoku() { Grid = sudoku };

            _sudokuProvider.SaveSudoku(sudokuToSave, _appConfig.SaveFolder);
            return Ok();
        }

        [HttpGet("loadgame/{gameName}")]
        [EnableCors]
        public ActionResult<Domain.Sudoku> LoadGame(string gameFile)
        {
            return _sudokuProvider.LoadSudoku(gameFile);
        }

        [HttpGet("loadgame")]
        [EnableCors]
        public ActionResult<Grid> LoadGame()
        {
            return _sudokuProvider.LoadLatestSudoku(_appConfig.SaveFolder).Grid;
        }

        [HttpPost("solve-next-step")]
        [EnableCors]
        public ActionResult<Cell> SolveNextStep([FromBody] Grid sudoku)
        {
            return _sudokuSolver.SolveNextStep(sudoku);
        }

        [HttpPost("quick-solve-notes")]
        [EnableCors]
        public ActionResult<Grid> QuickSolveNotes([FromBody] Grid sudoku)
        {
            return _sudokuSolver.QuickSolveNotes(sudoku);
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
