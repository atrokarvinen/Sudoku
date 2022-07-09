import { CellUI } from "../../models/CellUI";
import "./Grid.css";

interface CellProps {
  cell: CellUI;
  onNumberChanged: (value: number) => void;
  onErased(): void;
  onClicked(): void;
}

export default function Cell(props: CellProps) {
  const { cell, onNumberChanged, onClicked, onErased } = props;
  const { number, isPrefilled, row, column, notes } = cell;

  const numberStyle = isPrefilled ? "pre-filled" : "user-filled";

  const enforcedLines = [3, 6];
  const enforceRow = enforcedLines.includes(row) ? "row-enforced" : "";
  const enforceColumn = enforcedLines.includes(column) ? "column-enforced" : "";

  const allowedNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];

  const getTextLastNumber = (text: string): number => {
    console.log(`Parsing last number of text '${text}'...`);
    const lastLetter = text[text.length - 1];
    const lastNumber = +lastLetter;
    console.log(`Parsed last number: ${lastNumber}`);
    return lastNumber;
  };

  const handleCellNumberInput = (text: string) => {
    let numberInput: number = +text;

    if (isNaN(numberInput)) {
      console.log(`Input '${text}' is not a number`);
      return;
    }

    if (!allowedNumbers.includes(numberInput)) {
      console.log(
        `Input '${numberInput}' is not a valid number integer in range [1...9]. Attempting to parse the last number of the text as input...`
      );

      numberInput = getTextLastNumber(text);
      if (!allowedNumbers.includes(numberInput)) {
        console.log(
          `Input '${numberInput}' is still not a valid number integer in range [1...9]`
        );
        return;
      }
    }

    onNumberChanged(numberInput);
  };

  const handleCellTextChanged = (text: string) => {
    const isEmptyString = text.length === 0;
    console.log("text changed");
    if (isEmptyString) {
      onErased();
    } else {
      handleCellNumberInput(text);
    }
  };

  return (
    <div className={`cell ${enforceRow} ${enforceColumn}`} onClick={onClicked}>
      <input
        className={`cell__textbox ${numberStyle}`}
        type="text"
        value={number ?? ""}
        onChange={(e) => handleCellTextChanged(e.target.value)}
        readOnly={isPrefilled}
      />
      <div className="cell-notes">
        {notes?.map((note) => (
          <span key={note} className={`note-number-${note}`}>
            {note}
          </span>
        ))}
      </div>
    </div>
  );
}
