import React from "react";
import CellType from "../models/CellType";
import SudokuGrid from "../models/GripType";
import Cell from "./Cell";

interface RowProps {
  row: number;
  columnCount: number;
  sudokuGrid: SudokuGrid;
  
  cellClicked: (cell: CellType) => void;
}

export default function Row(props: RowProps) {
  const { row, columnCount } = props;

  return (
    <div style={{ display: "flex" }}>
      {[...Array.from(Array(columnCount).keys())].map(
        (column: number, index) => (
          <Cell
            key={index}
            row={row}
            column={column}
            number={props.sudokuGrid.rows[row].cells[column].number}

            cellClicked={props.cellClicked}
          />
        )
      )}
    </div>
  );
}
