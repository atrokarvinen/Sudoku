import { emptySudokuGrid } from "../models/GridType";
import {
  noteAdded,
  noteRemoved,
  numberAdded,
  numberRemoved,
  sudokuReducer,
} from "./sudokuSlice";

it.only("adds number", () => {
  const initialState = emptySudokuGrid();
  const sudokuState = sudokuReducer(
    initialState,
    numberAdded({ row: 2, column: 3, number: 5 })
  );
  expect(sudokuState.cells[2][3].number).toBe(5);
});

it.only("removes number", () => {
  const initialState = emptySudokuGrid();
  initialState.cells[2][3].number = 9;
  const sudokuState = sudokuReducer(
    initialState,
    numberRemoved({ row: 2, column: 3 })
  );
  expect(sudokuState.cells[2][3].number).toBeUndefined();
});

it.only("adds note", () => {
  const initialState = emptySudokuGrid();
  const sudokuState = sudokuReducer(
    initialState,
    noteAdded({ row: 2, column: 3, number: 5 })
  );
  expect(sudokuState.cells[2][3].notes).toContain(5);
});

it.only("removes note", () => {
  const initialState = emptySudokuGrid();
  initialState.cells[2][3].notes = [9];
  const sudokuState = sudokuReducer(
    initialState,
    noteRemoved({ row: 2, column: 3, number: 9 })
  );
  expect(sudokuState.cells[2][3].notes).not.toContain(9);
});
