import "./Menu.css";

import * as React from "react";

export interface MenuProps {
    saveGame: () => void
    loadGame: () => void
    emptySudoku: () => void
    solveNextStep: () => void
    solve: () => void
    quickSolveNotes: () => void
}

export default function Menu(props: MenuProps) {
  return (
    <div className="menu">
      <div className="menu__buttons">
        <button onClick={props.saveGame}>Save game</button>
        <button onClick={props.loadGame}>Load game</button>
        <button onClick={props.emptySudoku}>Empty sudoku</button>
        <button onClick={props.solveNextStep}>Solve next step</button>
        <button onClick={props.solve}>Solve</button>
        <button onClick={props.quickSolveNotes}>Quick notes</button>
      </div>
    </div>
  );
}
