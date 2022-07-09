import "./Menu.css";

export interface MenuProps {
  saveGame: () => void;
  loadGame: () => void;
  emptySudoku: () => void;
  solveNextStep: () => void;
  solve: () => void;
  quickSolveNotes: () => void;

  isAddNoteToggled: boolean;
  toggleAddNote(): void;
}

export default function Menu(props: MenuProps) {
  const { isAddNoteToggled, toggleAddNote } = props;
  return (
    <div className="menu">
      <div className="menu__buttons">
        <button onClick={props.saveGame}>Save game</button>
        <button onClick={props.loadGame}>Load game</button>
        <button onClick={props.emptySudoku}>Empty sudoku</button>
        <button onClick={props.solveNextStep}>Solve next step</button>
        <button onClick={props.solve}>Solve</button>
        <button onClick={props.quickSolveNotes}>Quick notes</button>

        <button onClick={toggleAddNote}>
          {isAddNoteToggled ? "Add number" : "Add note"}
        </button>
      </div>
    </div>
  );
}
