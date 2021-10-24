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

        public Cell()
        {

        }

        public Cell(int row, int column, int number)
        {
            Row = row;
            Column = column;
            Number = number;
        }

        public override string ToString()
        {
            return $"({Column}, {Row}) = {Number?.ToString() ?? "-"}";
        }
    }
}
