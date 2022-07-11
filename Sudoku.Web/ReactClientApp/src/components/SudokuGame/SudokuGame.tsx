import { useEffect, useState } from "react";
import { CellUI } from "../../models/CellUI";
import { GridType } from "../../models/GridType";
import { Point } from "../../models/Point";
import Grid from "../SudokuGrid/Grid";

export interface SudokuGameProps {
  sudokuState: GridType;
  setSudokuState: (sudokuState: GridType) => void;

  isAddNoteToggled: boolean;
}

export function SudokuGame({
  isAddNoteToggled,
  sudokuState,
  setSudokuState,
}: SudokuGameProps) {
  const [selectedCell, setSelectedCell] = useState<CellUI | undefined>({
    row: 0,
    column: 0,
    notes: [],
  });
  console.log(`Selected cell: (${selectedCell?.column}, ${selectedCell?.row})`);

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

    setSudokuState(sudokuGrid);
  };

  useEffect(() => {
    populateSudoku();
  }, []);

  const cellNumberChanged = (point: Point, number: number) => {
    const { row, column } = point;
    console.log(`cell (${column}, ${row}) was changed. New value: ${number}`);

    const newSudokuState: GridType = {
      ...sudokuState,
      cells: sudokuState.cells.map((cellRow) =>
        cellRow.map((cellColumn) => {
          if (cellColumn.row !== row || cellColumn.column !== column)
            return cellColumn;

          if (isAddNoteToggled) {
            const noteExists = !!cellColumn.notes.find((n) => n === number);
            return {
              ...cellColumn,
              notes: noteExists
                ? cellColumn.notes.filter((n) => n !== number)
                : [...cellColumn.notes, number],
            };
          }
          return {
            ...cellColumn,
            number: number,
            notes: [],
          };
        })
      ),
    };

    setSudokuState(newSudokuState);
  };

  const handleCellErased = (point: Point) => {
    const { row, column } = point;

    const newSudokuState: GridType = {
      ...sudokuState,
      cells: sudokuState.cells.map((cellRow) =>
        cellRow.map((cellColumn) => {
          if (cellColumn.row !== row || cellColumn.column !== column)
            return cellColumn;
          return {
            ...cellColumn,
            number: undefined,
          };
        })
      ),
    };

    setSudokuState(newSudokuState);
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
