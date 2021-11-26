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
        public int? Number { get; set; }
        public List<int> Notes { get; set; } = new List<int>();

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
                Notes.Add(number);
        }

        public void RemoveNote(int number)
        {
            Notes.Remove(number);
        }

        public override string ToString()
        {
            return $"({Column}, {Row}) = {Number?.ToString() ?? "-"}";
        }

        internal void ResetNotes()
        {
            while (Notes.Count > 0)
            {
                RemoveNote(Notes.First());
            }
        }
    }
}
