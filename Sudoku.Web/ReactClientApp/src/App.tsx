import "./App.css";
import { Layout } from "./layout/Layout";
import Menu from './components/Menu/Menu';
import SudokuGame from './components/SudokuGame/SudokuGame';

function App() {
  const backendUrl = "https://localhost:44340"
  const controllerName = "Sudoku"
  const SAVE_GAME_URL = `${backendUrl}/${controllerName}/savegame`
  const LOAD_GAME_URL = `${backendUrl}/${controllerName}/loadgame`

  const saveGame = () => {
    console.log("Save game")
    fetch(SAVE_GAME_URL, {method: 'POST', mode:'cors', headers: {}})
    .then((value: Response) => value.json())
    .then(jsonValue => console.log(jsonValue))
    .catch((reason) => console.log(reason))
  }

  const  loadGame = () => {
    console.log("Load game")
    fetch(LOAD_GAME_URL)
    .then((value: Response) => value.json())
    .then(jsonValue => console.log(jsonValue))
    .catch((reason) => console.log(reason))
  }
 
  return (
    <Layout>
      <SudokuGame />
      <Menu 
        saveGame={saveGame}
        loadGame={loadGame}
      />
    </Layout>
  );
}

export default App;
