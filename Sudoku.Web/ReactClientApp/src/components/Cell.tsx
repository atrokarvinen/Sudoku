import CellType from "../models/CellType";
import "./Grid.css";

interface CellProps {
  row: number;
  column: number;

  number?: number;
  isUserFilled?: boolean;

  notes?: number[];

  cellClicked: (cell: CellType) => void;
}

export default function Cell(props: CellProps) {
  const { number, isUserFilled, notes, row, column } = props;

  const numberColor = isUserFilled ? "blue" : "black";
  // const noteNumbers = notes ?? [];
  // const hasNumber = number !== undefined;
  const enforcedLines = [3, 6];
  const enforceRow = enforcedLines.includes(row) ? "row-enforced" : "";
  const enforceColumn = enforcedLines.includes(column) ? "column-enforced" : "";

  // console.log(enforceRow);

  const handleCellTextChanged = (text: string) => {
    const numberInput: number = +text;

    if (isNaN(numberInput)) {
      console.log(`Input '${text}' is not a number`);
      return;
    }
    const allowedNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
    if (!allowedNumbers.includes(numberInput)) {
      console.log(`Input '${numberInput}' is not a valid number integer in range [1...9]`);
      return;
    }

    const cellValue: CellType = { ...props };
    cellValue.number = numberInput;

    props.cellClicked(cellValue)
  };

  return (
    <div className={`cell ${enforceRow} ${enforceColumn}`}>
      {/* <button className="cell__button" type="button" onClick={() => props.cellClicked({ ...props })}>
        <span style={{ color: numberColor }}>{number}</span>
      </button> */}
      <input className="cell__textbox"
        
        type="text"
        value={number}
        onChange={(e) => handleCellTextChanged(e.target.value)}
      />
    </div>
  );
}
