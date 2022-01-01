import { CellType } from "../../models/CellType";
import { GridType } from "../../models/GridType";
import Cell from "./Cell";

interface GridProps {
  sudokuGrid: GridType;

  cellClicked: (cell: CellType) => void;
}

export default function Grid(props: GridProps) {
  const { sudokuGrid } = props;

  const rowCount = sudokuGrid.cells.length;
  console.log("Rowcount:" + rowCount);

  return (
    <div>
      {sudokuGrid.cells.map((row: CellType[], rowIndex) => (
        <div className="sudoku-row">
          {row.map((cell: CellType, columnIndex) => {
            return (
              <Cell
                key={rowIndex * 100 + columnIndex}
                row={rowIndex}
                column={columnIndex}
                number={cell.number}
                cellClicked={props.cellClicked}
                notes={cell.notes}
              />
            );
          })}
        </div>
      ))}
    </div>
  );
}
