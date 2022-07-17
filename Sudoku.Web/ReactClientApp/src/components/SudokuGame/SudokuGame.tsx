import { useCallback, useEffect, useState } from "react";
import { CellUI } from "../../models/CellUI";
import { GridType } from "../../models/GridType";
import { Point } from "../../models/Point";
import { useAppDispatch } from "../../redux/store";
import {
  noteAdded,
  numberAdded,
  numberRemoved,
  sudokuFilled,
} from "../../redux/sudokuSlice";
import Grid from "../SudokuGrid/Grid";

export interface SudokuGameProps {
  sudokuState: GridType;
  isAddNoteToggled: boolean;
}

export function SudokuGame({ isAddNoteToggled, sudokuState }: SudokuGameProps) {
  const dispatch = useAppDispatch();

  const [selectedCell, setSelectedCell] = useState<CellUI | undefined>({
    row: 0,
    column: 0,
    notes: [],
  });
  // console.log(`Selected cell: (${selectedCell?.column}, ${selectedCell?.row})`);
  // console.log("[SudokuGame] isAddNoteToggled: " + isAddNoteToggled);

  const populateSudoku = () => {
    const sudokuGrid: GridType = { cells: [] };
    for (let row = 0; row < 9; row++) {
      const sudokuRow: CellUI[] = [];
      for (let column = 0; column < 9; column++) {
        const isEmpty = Math.random() < 0.5;
        sudokuRow.push({
          row: row,
          column: column,
          // number: isEmpty ? undefined : row + 1,
          number: isEmpty ? undefined : Math.round(Math.random() * 9),
          // notes: isEmpty ? [1, 2, 3] : [],
          notes: isEmpty ? [1, 2, 3, 4, 5, 6, 7, 8, 9] : [],
          isPrefilled: !isEmpty,
        });
      }
      sudokuGrid.cells.push(sudokuRow);
    }

    dispatch(sudokuFilled(sudokuGrid));
  };

  useEffect(() => {
    // populateSudoku();
  }, []);

  const cellNumberChanged = useCallback(
    (point: Point, number: number) => {
      const { row, column } = point;
      console.log(
        `cell (${column}, ${row}) ${
          isAddNoteToggled ? "note" : "number"
        } was changed. New value: ${number}`
      );

      if (isAddNoteToggled) {
        dispatch(noteAdded({ row, column, number }));
      } else {
        dispatch(numberAdded({ row, column, number }));
      }
    },
    [isAddNoteToggled]
  );

  if (selectedCell) {
    const { row, column } = selectedCell;
    console.log(
      "selected cell notes: " + sudokuState.cells[row][column].notes.join(", ")
    );
  }

  const handleCellErased = (point: Point) => {
    const { row, column } = point;
    console.log("erasing cell");

    dispatch(numberRemoved({ row, column }));
  };

  const handleCellClicked = (point: Point) => {
    const { row, column } = point;
    const clickedCell = sudokuState.cells[row][column];
    setSelectedCell(clickedCell);
  };

  return (
    <div className="App">
      <button onClick={populateSudoku}>Populate</button>
      <Grid
        sudokuGrid={sudokuState}
        cellNumberChanged={cellNumberChanged}
        cellErased={handleCellErased}
        cellClicked={handleCellClicked}
      />
    </div>
  );
}

export default SudokuGame;
