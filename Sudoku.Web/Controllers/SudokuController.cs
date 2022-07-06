using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Domain;
using Sudoku.Services;
using Sudoku.Tests.Saved_test_sudokus;
using Sudoku.Tests.Utils;
using Sudoku.Web.Models;

namespace Sudoku.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly ISudokuSolver _sudokuSolver;
        private readonly ApplicationConfiguration _appConfig = new ApplicationConfiguration();

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
            Domain.Sudoku game = _sudokuProvider.LoadSudoku(gameFile);
            return new OkObjectResult(game);
        }

        [HttpGet("loadgame")]
        [EnableCors]
        public ActionResult<Grid> LoadGame()
        {
            //Grid grid = _sudokuProvider.LoadLatestSudoku(_appConfig.SaveFolder).Grid;
            //Grid expertSudoku = SudokuGenerator.FromText(TestSudokuFixtures.ExpertSudoku.Sudoku);
            Grid expertSudoku = SudokuGenerator.FromText(TestSudokuFixtures.EvilSudoku.Sudoku);
            Grid grid = expertSudoku;
            return new OkObjectResult(grid);
        }

        [HttpPost("solve-next-step")]
        [EnableCors]
        public ActionResult<Cell> SolveNextStep([FromBody] Grid sudoku)
        {
            Cell cell = _sudokuSolver.SolveNextStep(sudoku);
            return new OkObjectResult(cell);
        }

        [HttpPost("quick-solve-notes")]
        [EnableCors]
        public ActionResult<Grid> QuickSolveNotes([FromBody] Grid sudoku)
        {
            Grid grid = _sudokuSolver.QuickSolveNotes(sudoku);
            return new OkObjectResult(grid);
        }

        [HttpPost("solve")]
        [EnableCors]
        public ActionResult<Grid> Solve([FromBody] Grid sudoku)
        {
            Grid grid = _sudokuSolver.Solve(new Domain.Sudoku() { Grid = sudoku });
            return new OkObjectResult(grid);
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
