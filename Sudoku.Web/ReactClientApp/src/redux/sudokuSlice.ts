import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { emptySudokuGrid, GridType } from "../models/GridType";
import { Point } from "../models/Point";

const initialState: GridType = emptySudokuGrid();

type Marking = {
  row: number;
  column: number;
  number: number;
};

const sudokuSlice = createSlice({
  name: "sudoku",
  initialState,
  reducers: {
    numberAdded: (state, action: PayloadAction<Marking>) => {
      const { column, row, number } = action.payload;
      state.cells[row][column].number = number;
      state.cells[row][column].notes = [];
    },
    numberRemoved: (state, action: PayloadAction<Point>) => {
      const { column, row } = action.payload;
      state.cells[row][column].number = undefined;
    },
    noteAdded: (state, action: PayloadAction<Marking>) => {
      const { column, row, number } = action.payload;
      const notes = state.cells[row][column].notes;
      const noteExists = notes.includes(number);
      if (noteExists) {
        notes.splice(notes.indexOf(number), 1);
      } else {
        notes.push(number);
      }
    },
    noteRemoved: (state, action: PayloadAction<Marking>) => {
      const { column, row, number } = action.payload;
      const notes = state.cells[row][column].notes;
      if (!notes.includes(number)) return;
      notes.splice(notes.indexOf(number), 1);
    },
    sudokuEmptied: () => initialState,
    sudokuFilled: (state, action: PayloadAction<GridType>) =>
      (state = action.payload),
  },
});

export const {
  noteAdded,
  noteRemoved,
  numberAdded,
  numberRemoved,
  sudokuEmptied,
  sudokuFilled,
} = sudokuSlice.actions;

export const sudokuReducer = sudokuSlice.reducer;
