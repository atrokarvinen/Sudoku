import CellType from "../../models/CellType";
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
  const { number, isUserFilled, row, column, notes } = props;

  const numberStyle = isUserFilled ? "user-filled" : "pre-filled";
  // const noteNumbers = notes ?? [];
  // const hasNumber = number !== undefined;
  const enforcedLines = [3, 6];
  const enforceRow = enforcedLines.includes(row) ? "row-enforced" : "";
  const enforceColumn = enforcedLines.includes(column) ? "column-enforced" : "";

  const allowedNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];

  console.log(`Cell coordinates: (c x r) = (${column}, ${row})`);
  const handleCellErase = () => {
    const cellValue: CellType = { ...props };
    cellValue.number = undefined;

    props.cellClicked(cellValue);
  };

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

    const cellValue: CellType = { ...props };
    cellValue.number = numberInput;

    props.cellClicked(cellValue);
  };

  const handleCellTextChanged = (text: string) => {
    const isEmptyString: boolean = text.length === 0;
    if (isEmptyString) {
      handleCellErase();
    } else {
      handleCellNumberInput(text);
    }
  };

  const displayNotes = () => {
    return (
      <div className="cell-notes">
        {notes?.map(note => <span className={`note-number-${note}`}>{note}</span>)}
      </div>
    )
  }

  return (
    <div className={`cell ${enforceRow} ${enforceColumn}`}>
      {/* <button className="cell__button" type="button" onClick={() => props.cellClicked({ ...props })}>
        <span style={{ color: numberColor }}>{number}</span>
      </button> */}
      {displayNotes()}
      <input
        className={`cell__textbox ${numberStyle}`}
        type="text"
        value={number ?? ''}
        onChange={(e) => handleCellTextChanged(e.target.value)}
      />
    </div>
  );
}
