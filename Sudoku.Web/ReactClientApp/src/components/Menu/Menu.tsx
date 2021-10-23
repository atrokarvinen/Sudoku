import "./Menu.css";

import * as React from "react";

export interface MenuProps {
    saveGame: () => void
    loadGame: () => void
}

export default function Menu(props: MenuProps) {
  return (
    <div className="menu">
      <div className="menu__buttons">
        <button onClick={props.saveGame}>Save game</button>
        <button onClick={props.loadGame}>Load game</button>
      </div>
    </div>
  );
}
