using Sudoku.Services.Math;

namespace Sudoku.Tests.MathTests;

public class CombinatoricsTests
{
    [Fact]
    public void GetsAllSubsets_SetOfThree_ReturnsPairs()
    {
        List<int> list = new List<int>() { 1, 2, 3 };
        Combinatorics combinatorics = new Combinatorics();

        var subsets = combinatorics.GetAllSubsets(list, 2);

        List<List<int>> expectedSubsets = new List<List<int>>()
        {
            new () { 1, 2 },
            new () { 1, 3 },
            new () { 2, 3 },
        };
        subsets.Should().HaveCount(3);
        subsets.Should().BeEquivalentTo(expectedSubsets);
    }

    [Fact]
    public void GetsAllSubsets_SetOfFour_ReturnsPairs()
    {
        List<int> list = new List<int>() { 1, 2, 3, 4 };
        Combinatorics combinatorics = new Combinatorics();

        var subsets = combinatorics.GetAllSubsets(list, 2);

        List<List<int>> expectedSubsets = new List<List<int>>()
        {
            new () { 1, 2 },
            new () { 1, 3 },
            new () { 1, 4 },
            new () { 2, 3 },
            new () { 2, 4 },
            new () { 3, 4 },
        };
        subsets.Should().HaveCount(6);
        subsets.Should().BeEquivalentTo(expectedSubsets);
    }

    [Fact]
    public void GetsAllSubsets_SetOfFour_ReturnsTriplets()
    {
        List<int> list = new List<int>() { 1, 2, 3, 4 };
        Combinatorics combinatorics = new Combinatorics();

        var subsets = combinatorics.GetAllSubsets(list, 3);

        List<List<int>> expectedSubsets = new List<List<int>>()
        {
            new () { 1, 2, 3, },
            new () { 1, 2, 4, },
            new () { 1, 3, 4, },
            new () { 2, 3, 4, },
        };
        subsets.Should().HaveCount(4);
        subsets.Should().BeEquivalentTo(expectedSubsets);
    }

    [Fact]
    public void GetsAllSubsets_SetOfFour_ReturnsQuadruplets()
    {
        List<int> list = new List<int>() { 1, 2, 3, 4 };
        Combinatorics combinatorics = new Combinatorics();

        var subsets = combinatorics.GetAllSubsets(list, 4);

        List<List<int>> expectedSubsets = new List<List<int>>()
        {
            new () { 1, 2, 3, 4 },
        };
        subsets.Should().HaveCount(1);
        subsets.Should().BeEquivalentTo(expectedSubsets);
    }

    [Fact]
    public void GetAllSubsets_GridPointSet_ReturnsPairs()
    {
        List<GridPoint> list = new List<GridPoint>()
        {
            new (0, 0),
            new (0, 1),
            new (0, 2),
        };
        Combinatorics combinatorics = new Combinatorics();

        var subsets = combinatorics.GetAllSubsets(list, 2);

        List<List<GridPoint>> expectedSubsets = new List<List<GridPoint>>()
        {
            new () { new (0, 0), new (0, 1) },
            new () { new (0, 0), new (0, 2) },
            new () { new (0, 1), new (0, 2) },
        };
        subsets.Should().HaveCount(expectedSubsets.Count);
        subsets.Should().BeEquivalentTo(expectedSubsets);
    }
}
