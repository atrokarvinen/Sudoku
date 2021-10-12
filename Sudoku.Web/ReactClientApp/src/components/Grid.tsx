import CellType from "../models/CellType";
import SudokuGrid from "../models/GripType";
import Row from "./Row";

interface GridProps {
  rowCount: number;
  columnCount: number;
  sudokuGrid: SudokuGrid;

	cellClicked: (cell: CellType) => void;
}

export default function Grid(props: GridProps) {
  const { rowCount, columnCount } = props;

  return (
    <div>
      {[...Array.from(Array(rowCount).keys())].map((row: number, index) => {
        // console.log(`row: ${row}`);
        return (
          <Row
            key={index}
            row={row}
            columnCount={columnCount}
            sudokuGrid={props.sudokuGrid}

						cellClicked={props.cellClicked}
          />
        );
      })}
    </div>
  );
}
