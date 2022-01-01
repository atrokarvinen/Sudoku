using Sudoku.Domain;
using Sudoku.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sudoku.Tests
{
    public class SudokuRulesTest
    {
        private const string SimpleSudokuFile = "Sudoku_simple.txt";
        private readonly ISudokuRules _SudokuRules;
        private readonly Grid Grid;

        public SudokuRulesTest()
        {
            _SudokuRules = new StandardSudokuRules();
            Domain.Sudoku sudoku = SudokuTestUtils.LoadSudokuFromFile(SimpleSudokuFile);
            Grid = sudoku.Grid;
        }

        [Fact]
        public void CellIsEmpty()
        {
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 0)));
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 1)));
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 2)));
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 3)));
            Assert.False(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 4)));
            Assert.False(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 5)));
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 6)));
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 7)));
            Assert.True(_SudokuRules.IsCellEmpty(Grid, new GridPoint(0, 8)));
        }

        [Fact]
        public void RowShouldAllowPlacement()
        {
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 2));
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 3));
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 4));
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 5));
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 6));
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 7));
            Assert.True(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 9));
        }

        [Fact]
        public void RowShouldNotAllowPlacement()
        {
            Assert.False(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 1));
            Assert.False(_SudokuRules.DoesRowAllowPlacement(Grid, 0, 8));
        }

        [Fact]
        public void ColumnShouldAllowPlacement()
        {
            Assert.True(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 2));
            Assert.True(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 6));
            Assert.True(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 7));
        }


        [Fact]
        public void ColumnShouldNotAllowPlacement()
        {
            Assert.False(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 1));
            Assert.False(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 3));
            Assert.False(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 4));
            Assert.False(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 5));
            Assert.False(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 8));
            Assert.False(_SudokuRules.DoesColumnAllowPlacement(Grid, 0, 9));
        }

        [Fact]
        public void GetCellsInBox_TopLeft()
        {
            List<Cell> boxCells = _SudokuRules.GetCellsInBox(Grid, new GridPoint(0, 0));

            Assert.True(boxCells.Count == 9);
            Assert.True(boxCells.Min(x => x.Row) == 0);
            Assert.True(boxCells.Max(x => x.Row) == 2);
            Assert.True(boxCells.Min(x => x.Column) == 0);
            Assert.True(boxCells.Max(x => x.Column) == 2);
        }

        [Fact]
        public void GetCellsInBox_Center()
        {
            List<Cell> boxCells = _SudokuRules.GetCellsInBox(Grid, new GridPoint(4, 4));

            Assert.True(boxCells.Count == 9);
            Assert.True(boxCells.Min(x => x.Row) == 3);
            Assert.True(boxCells.Max(x => x.Row) == 5);
            Assert.True(boxCells.Min(x => x.Column) == 3);
            Assert.True(boxCells.Max(x => x.Column) == 5);
        }

        [Fact]
        public void GetCellsInBox_BottomRight()
        {
            List<Cell> boxCells = _SudokuRules.GetCellsInBox(Grid, new GridPoint(7, 7));

            Assert.True(boxCells.Count == 9);
            Assert.True(boxCells.Min(x => x.Row) == 6);
            Assert.True(boxCells.Max(x => x.Row) == 8);
            Assert.True(boxCells.Min(x => x.Column) == 6);
            Assert.True(boxCells.Max(x => x.Column) == 8);
        }

        [Fact]
        public void BoxShouldAllowPlacement()
        {
            Assert.True(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 1));
            Assert.True(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 2));
            Assert.True(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 4));
            Assert.True(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 7));
        }



        [Fact]
        public void BoxShouldNotAllowPlacement()
        {
            Assert.False(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 3));
            Assert.False(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 5));
            Assert.False(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 6));
            Assert.False(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 8));
            Assert.False(_SudokuRules.DoesBoxAllowPlacement(Grid, new GridPoint(0, 0), 9));
        }

        [Fact]
        public void CellShouldAllowPlacement()
        {
            Assert.True(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 2));
            Assert.True(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 7));

        }


        [Fact]
        public void CellShouldNotAllowPlacement()
        {
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 1));
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 3));
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 4));
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 5));
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 6));
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 8));
            Assert.False(_SudokuRules.CanNumberBePlaced(Grid, new GridPoint(0, 0), 9));
        }

        [Fact]
        public void GetRelatedCells()
        {
            List<Cell> relatedCells = _SudokuRules.GetRelatedCells(Grid, new GridPoint(0, 0));

            Assert.True(relatedCells.Count == (3 * 3) + 6 + 6);

            List<GridPoint> expectedGridPoints = new List<GridPoint>()
            {
                // Box
                new GridPoint(0,0), new GridPoint(0,1), new GridPoint(0,2),
                new GridPoint(1,0), new GridPoint(1,1), new GridPoint(1,2),
                new GridPoint(2,0), new GridPoint(2,1), new GridPoint(2,2),

                // Column
                new GridPoint(0,3),
                new GridPoint(0,4),
                new GridPoint(0,5),
                new GridPoint(0,6),
                new GridPoint(0,7),
                new GridPoint(0,8),

                // Row
                new GridPoint(3,0),
                new GridPoint(4,0),
                new GridPoint(5,0),
                new GridPoint(6,0),
                new GridPoint(7,0),
                new GridPoint(8,0),

            };

            foreach (var gridPoint in expectedGridPoints)
            {
                Assert.Single(relatedCells, gridPoint);
            }
        }
    }
}
