import React, { useEffect, useState } from "react";
import "./App.css";
import Grid from "./components/Grid";
import Row from "./models/RowType";
import SudokuGrid, { EmptySudokuGrid } from "./models/GripType";
import Cell from "./models/CellType";

function App() {
  const [sudokuState, setSudokuState] = useState<SudokuGrid>(EmptySudokuGrid());
  const [selectedCell, setSelectedCell] = useState<Cell | undefined>({
    row: 0,
    column: 0,
  });

  const populateSudoku = () => {
    const sudokuGrid: SudokuGrid = { rows: [] };
    for (let row = 0; row < 9; row++) {
      const sudokuRow: Row = { cells: [] };
      for (let column = 0; column < 9; column++) {
        sudokuRow.cells.push({ row: row, column: column, number: row + 1 });
      }
      sudokuGrid.rows.push(sudokuRow);
    }

    printSudoku(sudokuGrid);
    setSudokuState(sudokuGrid);
  }

  useEffect(() => {
    // populateSudoku()
  }, []);

  console.log(`Selected cell: (${selectedCell?.column}, ${selectedCell?.row})`);

  const cellClicked = (cell: Cell) => {
    const { row, column } = cell;
    console.log(`cell (${column}, ${row}) was clicked. New value: ${cell.number}`);
    const newSudokuState = {...sudokuState};

    newSudokuState.rows[cell.row].cells[cell.column].number = cell.number;

    setSelectedCell({ ...cell });
    setSudokuState(newSudokuState)
  };

  const keyDownListener = (e: KeyboardEvent, selectedCell: Cell | undefined) => {
    if (selectedCell === undefined) {
      console.log("No cell is selected");
      return;
    }

    const pressedKey = e.key;
    console.log(`Pressed key: '${pressedKey}'`);

    const numberInput: number = +pressedKey;

    if (isNaN(numberInput)) {
      console.log(`Input '${pressedKey}' is not a number`);
      return;
    }
    const allowedNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
    if (!allowedNumbers.includes(numberInput)) {
      console.log(
        `Input '${numberInput}' is not a valid number integer in range [1...9]`
      );
      return;
    }

    const newSudokuState: SudokuGrid = { rows: [] };
    for (let row = 0; row < sudokuState.rows.length; row++) {
      const newSudokuRow: Row = { cells: [] };
      const sudokuRow = sudokuState.rows[row];
      for (let column = 0; column < sudokuRow.cells.length; column++) {
        const sudokuCell: Cell = sudokuRow.cells[column];
        console.log(`cell ${column}, ${row} = ${sudokuCell.number}`)
        newSudokuRow.cells.push({ ...sudokuCell });
      }
      newSudokuState.rows.push(newSudokuRow);
    }

    newSudokuState.rows[selectedCell.row].cells[selectedCell.column].number =
      numberInput;

    console.log("\nNew sudoku:");
    printSudoku(newSudokuState);

    console.log(`Selected cell to change: (${selectedCell?.column}, ${selectedCell?.row})`);
    // setSudokuState(newSudokuState);
  };

  const printSudoku = (sudoku: SudokuGrid) => {
    for (let row = 0; row < sudoku.rows.length; row++) {
      let rowStr: string = "";
      for (let column = 0; column < sudoku.rows[row].cells.length; column++) {
        rowStr = rowStr + sudoku.rows[row].cells[column].number;
      }
      console.log(rowStr);
    }
  };

  // console.log("\nOld sudoku:");
  // printSudoku(sudokuState);

  return (
    <div className="App">
      <button onClick={populateSudoku}>Populate</button>
      <Grid
        rowCount={9}
        columnCount={9}
        sudokuGrid={sudokuState}
        cellClicked={cellClicked}
      />
    </div>
  );
}

export default App;
