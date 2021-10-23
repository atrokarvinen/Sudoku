import CellType from "./CellType";

export default interface GridType {
    cells: CellType[][]
}

export const EmptySudokuGrid = (): GridType => {
    const sudokuGrid: GridType = { cells: [] };
    for (let row = 0; row < 9; row++) {
      const sudokuRow: CellType[] = [];
      for (let column = 0; column < 9; column++) {
        sudokuRow.push({ row: row, column: column });
      }
      sudokuGrid.cells.push(sudokuRow);
    }

    return sudokuGrid;
}