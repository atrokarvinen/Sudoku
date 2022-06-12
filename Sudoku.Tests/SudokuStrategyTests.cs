using Sudoku.Services.Strategies;

namespace Sudoku.Tests
{
    public class SudokuStrategyTests
    {
        private readonly string sudokuText =
          @" 12| 5 |  3
            3  |   |   
               |   |   
            -----------
               |   |   
               |   |   
               |   |   
            -----------
               |   |   
               |   |   
              9|   |   ";

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(0, 2, 2)]
        [InlineData(0, 4, 5)]
        [InlineData(0, 8, 3)]
        [InlineData(1, 0, 3)]
        [InlineData(8, 2, 9)]
        public void Convert_SudokuFromText_ConvertsCorrectNumbers(int row, int column, int number)
        {
            Grid testGrid = SudokuFromText.Convert(sudokuText);
            testGrid.GetCell(row, column).Number.Should().Be(number);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 7, 2)]
        [InlineData(0, 5, 5)]
        [InlineData(8, 8, 9)]
        public void RowScanStrategy_EliminatesSingleNote(int row, int column, int number)
        {
            Grid testGrid = SudokuFromText.Convert(sudokuText);
            GridPoint targetPoint = new(row, column);
            testGrid.SetCellNote(targetPoint, number);
            RowScanStrategy strategy = new RowScanStrategy();

            IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(testGrid);

            solutions.Should().HaveCount(1);
            solutions.First().GridPoint.Should().Be(targetPoint);
            solutions.First().Number.Should().Be(number);
        }

        [Fact]
        public void RowScanStrategy_EliminatesMultipleNotes()
        {
            Grid testGrid = SudokuFromText.Convert(sudokuText);
            List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new Elimination(0, 0, 1),
                new Elimination(0, 7, 2),
                new Elimination(0, 5, 5),
                new Elimination(8, 8, 9),
            };
            expectedSolutions.ForEach(e => testGrid.SetCellNote(e.GridPoint, e.Number));
            RowScanStrategy strategy = new RowScanStrategy();

            IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(testGrid);

            solutions.Should().HaveCount(expectedSolutions.Count);
            solutions.Should().BeEquivalentTo(expectedSolutions);
        }
    }
}
