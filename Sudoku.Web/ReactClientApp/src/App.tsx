import "./App.css";
import { Layout } from "./layout/Layout";
import Menu from "./components/Menu/Menu";
import SudokuGame from "./components/SudokuGame/SudokuGame";
import React from "react";
import GridType, { EmptySudokuGrid } from "./models/GripType";

function App() {
  const backendUrl = "https://localhost:44340";
  const controllerName = "Sudoku";
  const SAVE_GAME_URL = `${backendUrl}/${controllerName}/savegame`;
  const LOAD_GAME_URL = `${backendUrl}/${controllerName}/loadgame`;

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
        console.log('Loaded game successfully.')
        setSudokuState(loadedSudoku)
      })
      .catch((reason) => console.log(reason));
  };

  return (
    <Layout>
      <SudokuGame sudokuState={sudokuState} setSudokuState={setSudokuState} />
      <Menu saveGame={saveGame} loadGame={loadGame} />
    </Layout>
  );
}

export default App;
