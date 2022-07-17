import { useEffect } from "react";
import { CellUI } from "../../models/CellUI";
import "./Cell.css";

interface CellProps {
  cell: CellUI;

  isRowHighlighted: boolean;
  isColumnHighlighted: boolean;
  highlightedNumber?: number;

  onNumberChanged: (value: number) => void;
  onErased(): void;
  onClicked(): void;
}

export default function Cell({
  cell,

  isRowHighlighted,
  isColumnHighlighted,
  highlightedNumber,

  onNumberChanged,
  onClicked,
  onErased,
}: CellProps) {
  const { number, isPrefilled, row, column, notes } = cell;

  const allowedNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];

  const isSelected = isRowHighlighted && isColumnHighlighted;
  const cellHighlighted = isRowHighlighted || isColumnHighlighted;
  const highlightCell = cellHighlighted ? "highlighted" : "";
  const isNumberHighlighted = highlightedNumber && highlightedNumber === number;
  const highlightNumber = isNumberHighlighted ? "highlighted" : "";
  const numberStyle = isPrefilled ? "pre-filled" : "user-filled";

  const enforcedLines = [3, 6];
  const enforceRow = enforcedLines.includes(row) ? "row-enforced" : "";
  const enforceColumn = enforcedLines.includes(column) ? "column-enforced" : "";

  useEffect(() => {
    if (isSelected) {
      document.addEventListener("keydown", keydownHandler);
    }

    return () => {
      if (isSelected) {
        console.log("remove event handler");
        document.removeEventListener("keydown", keydownHandler);
      }
    };
  }, [isSelected, onNumberChanged]);

  const keydownHandler = (ev: KeyboardEvent) => {
    if (isPrefilled) return;

    // console.log(`key '${ev.key}' pressed`);
    if (ev.key === "Backspace") {
      onErased();
      return;
    }

    const numberInput = +ev.key;
    if (Number.isNaN(numberInput) || !allowedNumbers.includes(numberInput))
      return;

    onNumberChanged(numberInput);
  };

  console.log("[Cell] updates");

  return (
    <div
      className={`cell ${enforceRow} ${enforceColumn} ${highlightCell}`}
      onClick={onClicked}
    >
      <input
        className={`cell__textbox ${numberStyle} ${highlightNumber}`}
        type="text"
        value={number ?? ""}
        readOnly={true}
      />
      <div className="cell-notes">
        {notes?.map((note) => {
          const highlightNote = highlightedNumber === note ? "highlighted" : "";
          return (
            <span
              key={note}
              style={{ gridArea: `number${note}` }}
              className={`note-number ${highlightNote}`}
            >
              {note}
            </span>
          );
        })}
      </div>
    </div>
  );
}
