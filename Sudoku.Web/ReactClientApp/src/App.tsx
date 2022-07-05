import "./App.css";
import { Layout } from "./layout/Layout";
import Menu from "./components/Menu/Menu";
import SudokuGame from "./components/SudokuGame/SudokuGame";
import React from "react";
import { CellType } from "./models/CellType";
import { GridType, EmptySudokuGrid } from "./models/GridType";

function App() {
  const backendUrl = "https://localhost:44340";
  const controllerName = "Sudoku";
  const SAVE_GAME_URL = `${backendUrl}/${controllerName}/savegame`;
  const LOAD_GAME_URL = `${backendUrl}/${controllerName}/loadgame`;
  const SOLVE_NEXT_STEP_URL = `${backendUrl}/${controllerName}/solve-next-step`;
  const QUICK_SOLVE_NOTES = `${backendUrl}/${controllerName}/quick-solve-notes`;
  const SOLVE = `${backendUrl}/${controllerName}/solve`;

  const [sudokuState, setSudokuState] = React.useState<GridType>(
    EmptySudokuGrid()
  );

  const saveGame = () => {
    console.log("Save game");
    const sudokuJson = JSON.stringify(sudokuState);
    // console.log(`Stringified sudoku:\n${sudokuJson}`);
    const url = `${SAVE_GAME_URL}`;
    fetch(url, {
      method: "POST",
      body: sudokuJson,
      headers: { "Content-Type": "application/json" },
    })
      .then((response: Response) => {
        response.ok
          ? console.log("Save was successfull.")
          : console.log("Save was not successfull.");
      })
      .catch((reason) => console.log(reason));
  };

  const loadGame = () => {
    console.log("Load game");
    fetch(LOAD_GAME_URL)
      .then((response: Response) => response.json())
      .then((loadedSudoku: GridType) => {
        console.log("Loaded game successfully.");
        setSudokuState(loadedSudoku);
      })
      .catch((reason) => console.log(reason));
  };

  const emptySudoku = () => {
    setSudokuState(EmptySudokuGrid());
  };

  const solveNextStep = () => {
    console.log("Solving next step of the sudoku...");
    const sudokuJson = JSON.stringify(sudokuState);
    fetch(SOLVE_NEXT_STEP_URL, {
      body: sudokuJson,
      headers: { "Content-Type": "application/json" },
      method: "POST",
    })
      .then((response: Response) => response.json())
      .then((solvedCell: CellType) => {
        const { row, column, number, notes } = solvedCell;
        console.log(
          `Solved step successfully. Next step: (${column}, ${row}) = ${number}`
        );
        const newSudokuState = { ...sudokuState };

        newSudokuState.cells[row][column].number = number;
        newSudokuState.cells[row][column].notes = notes;

        setSudokuState(newSudokuState);
      })
      .catch((reason) => console.log(reason));
  };

  const solve = () => {
    const sudokuJson = JSON.stringify(sudokuState);
    fetch(SOLVE, {
      body: sudokuJson,
      headers: { "Content-Type": "application/json" },
      method: "POST",
    })
      .then((response: Response) => response.json())
      .then((grid: GridType) => {
        setSudokuState(grid);
      })
      .catch((reason) => console.log(reason));
  };

  const quickSolveNotes = () => {
    const sudokuJson = JSON.stringify(sudokuState);
    fetch(QUICK_SOLVE_NOTES, {
      body: sudokuJson,
      headers: { "Content-Type": "application/json" },
      method: "POST",
    })
      .then((response: Response) => response.json())
      .then((grid: GridType) => {
        setSudokuState(grid);
      })
      .catch((reason) => console.log(reason));
  };

  return (
    <Layout>
      <SudokuGame sudokuState={sudokuState} setSudokuState={setSudokuState} />
      <Menu
        saveGame={saveGame}
        loadGame={loadGame}
        emptySudoku={emptySudoku}
        solveNextStep={solveNextStep}
        solve={solve}
        quickSolveNotes={quickSolveNotes}
      />
    </Layout>
  );
}

export default App;
