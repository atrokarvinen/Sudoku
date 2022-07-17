import { useState } from "react";
import { CellUI } from "../../models/CellUI";
import { GridType } from "../../models/GridType";
import { Point } from "../../models/Point";
import Cell from "./Cell";
import "./Grid.css";

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
  const [selectedCell, setSelectedCell] = useState<CellUI | undefined>();

  console.log("[Grid] updates");

  return (
    <div>
      {sudokuGrid.cells.map((row: CellUI[], rowIndex) => (
        <div key={rowIndex} className="sudoku-row">
          {row.map((cell: CellUI, columnIndex) => {
            const isRowHighlighted = rowIndex === selectedCell?.row;
            const isColumnHighlighted = columnIndex === selectedCell?.column;

            const point: Point = {
              row: rowIndex,
              column: columnIndex,
            };

            return (
              <Cell
                key={columnIndex}
                cell={cell}
                isRowHighlighted={isRowHighlighted}
                isColumnHighlighted={isColumnHighlighted}
                highlightedNumber={selectedCell?.number}
                onNumberChanged={(number) => cellNumberChanged(point, number)}
                onClicked={() => {
                  const { row, column } = point;
                  const clickedCell = sudokuGrid.cells[row][column];
                  setSelectedCell(clickedCell);
                  cellClicked(point);
                }}
                onErased={() => cellErased(point)}
              />
            );
          })}
        </div>
      ))}
    </div>
  );
}
