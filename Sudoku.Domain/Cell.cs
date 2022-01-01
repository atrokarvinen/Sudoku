using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public GridPoint GridPoint => new GridPoint(Row, Column);
        public int? Number { get; set; }
        public IEnumerable<int> Notes { get; set; } = new List<int>();

        public Cell()
        {

        }

        public Cell(int row, int column, int? number)
        {
            Row = row;
            Column = column;
            Number = number;
        }

        public void AddNote(int number)
        {
            if (!Notes.Contains(number))
                Notes = Notes.Append(number);
        }

        public void RemoveNote(int number)
        {
            List<int> noteList = Notes.ToList();
            noteList.Remove(number);
            Notes = noteList;
        }

        public void ResetNotes()
        {
            while (Notes.Count() > 0)
            {
                RemoveNote(Notes.First());
            }
        }

        public override string ToString()
        {
            return $"({Column}, {Row}) = {Number?.ToString() ?? "-"}";
        }
    }
}
