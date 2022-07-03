namespace Sudoku.Tests.Utils;

public class SudokuFromTextTests
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
        Grid testGrid = SudokuGenerator.FromText(sudokuText);
        testGrid.GetCell(row, column).Number.Should().Be(number);
    }
}
