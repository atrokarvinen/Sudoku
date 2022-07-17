import { configureStore } from "@reduxjs/toolkit";
import { useDispatch } from "react-redux";
import { sudokuReducer } from "./sudokuSlice";

const store = configureStore({
  reducer: {
    sudoku: sudokuReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;

export default store;

export type AppDispatch = typeof store.dispatch;
export const useAppDispatch: () => AppDispatch = useDispatch;
