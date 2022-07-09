import { CellUI } from "../../models/CellUI";
import { GridType } from "../../models/GridType";
import { Point } from "../../models/Point";
import Cell from "./Cell";

interface GridProps {
  sudokuGrid: GridType;

  cellNumberChanged(point: Point, number: number): void;
  cellErased(point: Point): void;
  cellClicked(point: Point): void;
}

export default function Grid({
  sudokuGrid,
  cellErased,
  cellNumberChanged,
  cellClicked,
}: GridProps) {
  return (
    <div>
      {sudokuGrid.cells.map((row: CellUI[], rowIndex) => (
        <div key={rowIndex} className="sudoku-row">
          {row.map((cell: CellUI, columnIndex) => {
            const point: Point = {
              row: rowIndex,
              column: columnIndex,
            };
            return (
              <Cell
                key={columnIndex}
                cell={cell}
                onNumberChanged={(number) => cellNumberChanged(point, number)}
                onClicked={() => cellClicked(point)}
                onErased={() => cellErased(point)}
              />
            );
          })}
        </div>
      ))}
    </div>
  );
}
