import * as React from "react";
import { CellType } from "../../models/CellType";
import { GridType } from "../../models/GridType";
import Grid from "../SudokuGrid/Grid";

export interface SudokuGameProps {
  sudokuState: GridType;
  setSudokuState: (sudokuState: GridType) => void;
}

export function SudokuGame(props: SudokuGameProps) {
  const { sudokuState, setSudokuState } = props;
  const [selectedCell, setSelectedCell] = React.useState<CellType | undefined>({
    row: 0,
    column: 0,
  });

  const populateSudoku = () => {
    const sudokuGrid: GridType = { cells: [] };
    for (let row = 0; row < 9; row++) {
      const sudokuRow: CellType[] = [];
      for (let column = 0; column < 9; column++) {
        sudokuRow.push({
          row: row,
          column: column,
          number: row + 1,
          notes: [1, 2, 3],
        });
      }
      sudokuGrid.cells.push(sudokuRow);
    }

    printSudoku(sudokuGrid);
    setSudokuState(sudokuGrid);
  };

  React.useEffect(() => {
    // populateSudoku()
  }, []);

  console.log(`Selected cell: (${selectedCell?.column}, ${selectedCell?.row})`);

  const cellClicked = (cell: CellType) => {
    const { row, column } = cell;
    console.log(
      `cell (${column}, ${row}) was clicked. New value: ${cell.number}`
    );
    const newSudokuState = { ...sudokuState };

    newSudokuState.cells[cell.row][cell.column].number = cell.number;

    setSelectedCell({ ...cell });
    setSudokuState(newSudokuState);
  };

  const printSudoku = (sudoku: GridType) => {
    for (let row = 0; row < sudoku.cells.length; row++) {
      let rowStr: string = "";
      for (let column = 0; column < sudoku.cells[row].length; column++) {
        rowStr = rowStr + sudoku.cells[row][column].number;
      }
      console.log(rowStr);
    }
  };

  // console.log("\nOld sudoku:");
  // printSudoku(sudokuState);

  return (
    <div className="App">
      <button onClick={populateSudoku}>Populate</button>
      <Grid sudokuGrid={sudokuState} cellClicked={cellClicked} />
    </div>
  );
}

export default SudokuGame;
