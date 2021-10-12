import Row from "./RowType";

export default interface SudokuGrid {
    rows: Row[]
}

export const EmptySudokuGrid = (): SudokuGrid => {
    const sudokuGrid: SudokuGrid = { rows: [] };
    for (let row = 0; row < 9; row++) {
      const sudokuRow: Row = { cells: [] };
      for (let column = 0; column < 9; column++) {
        sudokuRow.cells.push({ row: row, column: column });
      }
      sudokuGrid.rows.push(sudokuRow);
    }

    return sudokuGrid;
}