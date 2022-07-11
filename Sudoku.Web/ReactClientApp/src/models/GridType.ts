import { CellUI } from "./CellUI";

export type GridType = {
  cells: CellUI[][];
};

export const emptySudokuGrid = (): GridType => {
  const sudokuGrid: GridType = { cells: [] };
  for (let row = 0; row < 9; row++) {
    const sudokuRow: CellUI[] = [];
    for (let column = 0; column < 9; column++) {
      sudokuRow.push({ row: row, column: column, notes: [] });
    }
    sudokuGrid.cells.push(sudokuRow);
  }

  return sudokuGrid;
};
