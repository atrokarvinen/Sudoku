export default interface CellType {
  row: number;
  column: number;

  number?: number;
  isPrefilled?: boolean;

  notes?: number[];
}