export default interface Cell {
  row: number;
  column: number;

  number?: number;
  isPrefilled?: boolean;

  notes?: number[];
}